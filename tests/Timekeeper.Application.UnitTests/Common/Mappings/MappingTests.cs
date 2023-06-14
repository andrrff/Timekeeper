using AutoMapper;
using Timekeeper.Domain.Entities;
using System.Runtime.Serialization;
using Timekeeper.Application.Common.Mappings;
using Timekeeper.Application.Timesheets.Queries.GetActivity;

namespace Timekeeper.Application.UnitTests.Common.Mappings;

public class MappingTests
{
    private readonly IConfigurationProvider _configuration;
    private readonly IMapper _mapper;

    public MappingTests()
    {
        _configuration = new MapperConfiguration(config =>
            config.AddProfile<MappingProfile>());

        _mapper = _configuration.CreateMapper();
    }

    [Test]
    public void ShouldHaveValidConfiguration()
    {
        _configuration.AssertConfigurationIsValid();
    }

    [Test]
    [TestCase(typeof(Activity), typeof(ActitvityDto))]
    public void ShouldSupportMappingFromSourceToDestination(Type source, Type destination)
    {
        var instance = GetInstanceOf(source);
        instance.GetType().GetProperty("TimesheetId")!.SetValue(instance, 1);

        _mapper.Map(instance, source, destination);

        // Assert.AreEqual(instance.GetType().GetProperty("TimesheetId")?.GetValue(instance), dto.GetType().GetProperty("TimesheetId")?.GetValue(dto));
    }

    private object GetInstanceOf(Type type)
    {
        if (type.GetConstructor(Type.EmptyTypes) != null)
            return Activator.CreateInstance(type)!;

        // Type without parameterless constructor
        return FormatterServices.GetUninitializedObject(type);
    }
}