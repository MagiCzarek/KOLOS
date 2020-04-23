using System;
using System.Collections.Generic;
using System.Data;
using KOLOS.Models;
using Microsoft.AspNetCore.Mvc;
namespace KOLOS.Services
{
    public class PresrciptionService : IPrescriptionService
    {
        private string conString =
            "Data Source=db-mssql16.pjwstk.edu.pl;Initial Catalog=s18542;User ID=apbds18542;Password=admin";

        public List<Prescription> GetAll(string sortBy, string sortDir)
        {
            var prescriptionInfo = new List<Prescription>();
            using (var connection = new SqlConnection(conString))
            using (var command = new SqlCommand())
            {

                command.Connection = connection;
                command.CommandText =
                    "Select prescription.IdPrescription, prescription.Date, prescription.DueDate, Patient.LastName, Doctor.LastName FROM prescription INNER JOIN Doctor ON prescription.IdDoctor = Doctor.IdDoctor INNER JOIN Patient ON prescription.IdPatient = Patient.IdPatient";

                    connection.Open();
                var dr = command.ExecuteReader();
                while (dr.Read())
                {
                    var info = new Prescription();
                    {
                        idPrescription = dr["idPrescription"].toString(),
                        date = DateTime.Parse(dr["date"].ToString()),
                        Duedate = DateTime.Parse(dr["DueDate"].ToString()),
                        idDoctor = dr["idDoctor"].toString(),
                        idPatient = dr["idPatient"].toString();
                    }
                    ;
                    prescriptionInfo.Add(info);


                }


            }

            return prescriptionInfo;

        }

        public bool AddPrescripton(Request request)
        {
            using (var connection = new SqlConnection(conString))
            using (var command = new SqlCommand())
            {
                connection.Open();
                var transaction = connection.BeginTransaction();
                command.Transaction = transaction;
                command.Connection = connection;
                command.CommandText =
                    "insert into Prescription(date, DueDate, IdPatient, IdDoctor) values (@Date, @DueDate, @IdPatient, @IdDoctor); SELECT SCOPE_IDENTITY()";
                
                command.CommandType = CommandType.Text;
                command.Parameters.AddWithValue("date", Prescription.date);
                command.Parameters.AddWithValue("DueDate", Prescription.DueDate);
                command.Parameters.AddWithValue("idPatient", Prescription.idPatient);
                command.Parameters.AddWithValue("idDoctor", Prescription.idDoctor);


               int x= command.ExecuteScalar();
                
                //zmaapowac do modelu prescription i zwrocic
                //nie zdazylem

                transaction.Commit();
                
            }

            return true;
        }

    }
}