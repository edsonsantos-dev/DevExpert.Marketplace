using DevExpert.Marketplace.Core.Data.Context;
using DevExpert.Marketplace.Core.Domain.Sellers;

namespace DevExpert.Marketplace.Core.Data.Repositories;

public class SellerRepository(MarketplaceContext context) 
    : Repository<Seller>(context), ISellerRepository;