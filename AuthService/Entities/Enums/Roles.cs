using System.Text.Json.Serialization;

namespace AuthService.Entities.Enums
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum Roles
    {
        Admin,
        Manager,
        Seller,
        AdmLab,
        HR,
        Lab,
        NotAssigned
        
        
    }
}