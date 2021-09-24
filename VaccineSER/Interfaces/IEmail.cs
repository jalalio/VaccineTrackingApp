using System.Threading.Tasks;
using VaccineSER.DTOs;

namespace VaccineSER.Interfaces
{
    public interface IEmail
    {
        Task Send(string emailAddress, string subject, string body, EmailOptionsDTO options);
    }
}