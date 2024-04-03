using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SPCaemucals.Backend.Controllers;
using SPCaemucals.Backend.Dto;
using SPCaemucals.Backend.Models;
using SPCaemucals.Data.Enum;
using SPCaemucals.Data.Identities;

public class ParcelService
{
    private readonly ApplicationDbContext context;

    public ParcelService(ApplicationDbContext context)
    {
        this.context = context;
    }

    public async Task<List<Parcel>> GetParcelsByStatus(ParcelStatus? status,string userId)
    {
        var query = context.Parcels.AsQueryable();

        if (status is not null)
        {
            query = query.Where(x=>x.ParcelStatus == status);
        }
            
        
        query = query.Where(x => x.SaleManId == userId);

        query = query
            .Include(x => x.Customer).ThenInclude(x => x.Addresses)
            .Include(x => x.Customer).ThenInclude(x => x.Addresses).ThenInclude(x => x.Province)
            .Include(x => x.Customer).ThenInclude(x => x.Addresses).ThenInclude(x => x.District)
            .Include(x => x.Customer).ThenInclude(x => x.Addresses).ThenInclude(x => x.SubDistrict)
            .Include(x => x.Customer).ThenInclude(x => x.Addresses).ThenInclude(x => x.PostalCode)
            .Include(x => x.DeliveryVendor)
            .Include(x => x.SaleMan)
            .Include(x => x.SaleMan).ThenInclude(x => x.Address)
            .Include(x => x.SaleMan).ThenInclude(x => x.Address).ThenInclude(x => x.Province)
            .Include(x => x.SaleMan).ThenInclude(x => x.Address).ThenInclude(x => x.District)
            .Include(x => x.SaleMan).ThenInclude(x => x.Address).ThenInclude(x => x.SubDistrict)
            .Include(x => x.SaleMan).ThenInclude(x => x.Address).ThenInclude(x => x.PostalCode);

        var result = await query.ToListAsync();
        return result;
    }

    public async Task<Parcel> updateTracking(TrackingModel model)
    {
        Parcel parcel = context.Parcels.SingleOrDefault(x => x.Id == model.ParcelId);

        if (parcel == null)
        {
            return parcel; 
        }
        
        parcel.VendorTrackingNo = model.TrackNO;
        await context.SaveChangesAsync();
        return parcel;
        
    }
}