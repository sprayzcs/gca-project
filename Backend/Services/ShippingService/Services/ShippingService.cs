using System.Security.Cryptography;
using System.Text;
using AutoMapper;
using Shared;
using Shared.Data;
using Shared.Infrastructure;
using Shared.Security;
using ShippingService.Data;
using ShippingService.Infrastructure;

namespace ShippingService.Services;

public class ShippingService : IShippingService
{
    private readonly IShippingRepository _shippingRepository;
    private readonly ISecuredMethodService _securedMethodService;
    private readonly INotificationHandler _notificationHandler;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public ShippingService(
        IShippingRepository shippingRepository,
        ISecuredMethodService securedMethodService,
        INotificationHandler notificationHandler,
        IUnitOfWork unitOfWork,
        IMapper mapper
        )
    {
        _shippingRepository = shippingRepository;
        _securedMethodService = securedMethodService;
        _notificationHandler = notificationHandler;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<ShipmentDto> CreateShipmentForOrderAsync(Guid orderId, int orderPrice, CancellationToken cancellationToken)
    {
        if (!_securedMethodService.CanAccess())
        {
            _notificationHandler.RaiseError(GenericErrorCodes.InsufficientPermissions);
            return new();
        }

        var existingShipment = await _shippingRepository.GetByOrderIdAsync(orderId, cancellationToken);
        if (existingShipment != null)
        {
            _notificationHandler.RaiseError(ShippingErrors.ShipmentAlreadyExists);
            return new();
        }

        int shippingPrice = 0;
        if (orderPrice <= 10000)
        {
            shippingPrice = 1000;
        }

        var shipmentNumber = GenerateShipmentNumber();
        var shipment = new Shipment(Guid.NewGuid(), orderId, shipmentNumber, shippingPrice);
        await _shippingRepository.AddAsync(shipment, cancellationToken);

        if (!await _unitOfWork.CommitAsync(cancellationToken))
        {
            return new();
        }

        return _mapper.Map<ShipmentDto>(shipment);
    }

    public async Task<ShipmentDto> GetShipmentByIdAsync(Guid shipmentId, CancellationToken cancellationToken)
    {
        var shipment = await _shippingRepository.GetByIdAsync(shipmentId, cancellationToken);
        if (shipment == null)
        {
            _notificationHandler.RaiseError(GenericErrorCodes.ObjectNotFound);
            return new();
        }

        return _mapper.Map<ShipmentDto>(shipment);
    }

    private static string GenerateShipmentNumber()
    {
        const string characters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        StringBuilder builder = new();
        for (int i = 0; i < 16; i++)
        {
            builder.Append(characters[RandomNumberGenerator.GetInt32(0, characters.Length)]);
        }
        return builder.ToString();
    }
}
