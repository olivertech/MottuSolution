using Mottu.Application.Interfaces;
using Mottu.Application.Requests.Base;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Mottu.Application.Requests
{
    public class NotificationRequest : BaseRequest, IRequest
    {
    }
}
