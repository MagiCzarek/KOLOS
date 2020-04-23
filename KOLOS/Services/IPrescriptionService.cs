using System.Collections.Generic;
using KOLOS.Models;
using KOLOS.Request;
namespace KOLOS.Services
{
    public interface IPrescriptionService
    {
        List<Prescription> GetAll(string sortBy, string sortDir);
        bool AddPrescription(Request request);
    }
}