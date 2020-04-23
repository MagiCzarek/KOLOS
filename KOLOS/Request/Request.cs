using System;

namespace KOLOS.Request
{
    public class Request
    {
        public string IdPrescription { get; set; }
        public DateTime DueDate{ get; set; }
        public string PatientLastName{ get; set; }
        public string DoctorLastName{ get; set; }
    }
}