namespace CRUD_Operation.Mapper
{
    public class LearnMapper : Profile
    {
        public LearnMapper()
        {
            CreateMap<EnrollStudentDto, LearnEntity>()
                .ReverseMap();

            CreateMap<UpdateGradeDto, LearnEntity>()
                .ReverseMap();
        }
    }
}
