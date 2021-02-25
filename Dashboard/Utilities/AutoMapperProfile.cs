using System;
using AutoMapper;
using BlockchainCore.Models;
using Dashboard.Models;

namespace Dashboard.Utilities
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Block, BlockDto>();
            CreateMap<BlockDto, Block>();
            CreateMap<Transaction, TransactionDto>();
            CreateMap<TransactionDto, Transaction>();
        }
    }
}
