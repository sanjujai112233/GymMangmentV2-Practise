using GymMangV2.Domain.Entities;

namespace GymMangV2.Application.Interfaces;

public interface IMembershipRepository
{
    Task<List<MemberShip>> GetExpiringMembershipsAsync();  //membership we go 
}