using AutoMapper;
using GymMangV2.Application.DTOs.Members;
using GymMangV2.Domain.Entities;

namespace GymMangV2.Application.Mappings;

public class MemberProfile : Profile
{
    public MemberProfile()
    {
        CreateMap<CreateMemberRequestDto, Member>();
        CreateMap<UpdateMemberRequestDto, Member>();
        CreateMap<Member, MemberResponseDto>()
        .ForMember(  // Formember: when the entites not same then then we have mange entites like that
            dest => dest.FullName,
            opt => opt.MapFrom(src => src.FullName))
            .ForMember(
                dest => dest.TrainerName,
                opt => opt.MapFrom(src =>
                src.Trainer != null ? src.Trainer.FullName : ""));
    }



}