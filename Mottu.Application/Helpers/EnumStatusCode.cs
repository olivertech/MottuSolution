namespace Mottu.Application.Helpers
{
    public enum EnumStatusCode
    {
        [EnumMember(Value = "Status200OK")]
        Status200OK = 1,
        [EnumMember(Value = "Status404NotFound")]
        Status404NotFound = 2,
        [EnumMember(Value = "Status400BadRequest")]
        Status400BadRequest = 3,
        [EnumMember(Value = "Status304NotModified")]
        Status304NotModified = 4,
        [EnumMember(Value = "Status500InternalServerError")]
        Status500InternalServerError = 5,
    }
}
