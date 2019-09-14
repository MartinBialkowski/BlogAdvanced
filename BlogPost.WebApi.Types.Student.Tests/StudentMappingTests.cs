using AutoMapper;
using BlogPost.WebApi.Types.Course;
using System;
using Xunit;

namespace BlogPost.WebApi.Types.Student.Tests
{
    public class StudentMappingTests: IDisposable
    {
        private readonly IMapper mapper;
        public StudentMappingTests()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<StudentMapping>();
                cfg.AddProfile<CourseMapping>();
            });

            mapper = config.CreateMapper();
        }

        public void Dispose()
        {
            Mapper.Reset();
        }

        [Fact]
        public void ShouldMapConfigurationBeValid()
        {
            mapper.ConfigurationProvider.AssertConfigurationIsValid();
        }
    }
}
