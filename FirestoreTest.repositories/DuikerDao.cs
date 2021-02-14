using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FirestoreTest.Domain;
using Google.Cloud.Firestore;
using Newtonsoft.Json;

namespace FirestoreTest.repositories
{
    public class DuikerDao
    {
        FirestoreDb db;
        
        //connectie maken met de firestore db
        public DuikerDao() {
            
            string path = AppDomain.CurrentDomain.BaseDirectory + @"mvc_test.json";
            Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", path);
            db = FirestoreDb.Create("mvctest-d4be6");
        }

        //doc toevoegen aan de collectie Duikers (hier gwn 1 "kolom" test met waarde test als vb)
        public void addDuiker() {           
            CollectionReference coll = db.Collection("Duikers");
            Dictionary<string, object> data = new Dictionary<string, object>()
            {
                { "test", "test"}
            };
            coll.AddAsync(data);

        }

        //alle duikers uit de db ophalen en omzetten naar duiker objecten
        public async Task<IEnumerable<Duiker>> getDuikers() {
            //snapshot van alle duikers
            Query query = db.Collection("Duikers");
            QuerySnapshot snapshot = await query.GetSnapshotAsync();

            List<Duiker> duikers = new List<Duiker>();
            //elke duiker omzetten naar een object en aan de lijst toevoegen (heel slecht, kan wss met automapper ofzo)
            foreach (DocumentSnapshot docSnap in snapshot) {
                if (docSnap.Exists) {
                    Dictionary<string, object> duikerDict = docSnap.ToDictionary();

                    //gbdatum omzetten van timestamp (firestore) naar een datetime
                    Timestamp ts = (Timestamp) duikerDict["geboorteDatum"];
                    DateTime gbDatum = ts.ToDateTime().ToLocalTime();

                    //lijst van nummerplaten omzetten naar lijst van strings (is nu een lijst van objecten ik weet niet waarom)
                    List<string> nummerplaten = new List<string>();
                    List<object> npList = (List<object>) duikerDict["nummerplaten"];

                    foreach (var nummerplaat in npList) {
                        nummerplaten.Add(nummerplaat.ToString());
                    }

                    //nieuwe duiker aanmaken met de waarden uit de db
                    Duiker duiker = new Duiker
                    {
                        adres = (string) duikerDict["adres"],
                        naam = (string) duikerDict["naam"],
                        email = (string) duikerDict["email"],
                        geboorteDatum = gbDatum,
                        gemeente = (string) duikerDict["gemeente"],
                        gsm = (string) duikerDict["gsm"],
                        heeftBeperking = (bool) duikerDict["heeftBeperking"],
                        id = Convert.ToInt32(duikerDict["id"]),
                        isBegeleider = (bool) duikerDict["isBegeleider"],
                        nummerplaten = nummerplaten,
                        postcode = Convert.ToInt32(duikerDict["postcode"]),
                    };

                    duikers.Add(duiker);
                }
            }

            return duikers;            
        }



        public async Task<IEnumerable<Duiker>> getDuikers2()
        {
            //snapshot van alle duikers
            Query query = db.Collection("Duikers");
            QuerySnapshot snapshot = await query.GetSnapshotAsync();

            List<Duiker> duikers = new List<Duiker>();
            //elke duiker omzetten naar een object en aan de lijst toevoegen (heel slecht, kan wss met automapper ofzo)
            foreach (DocumentSnapshot docSnap in snapshot)
            {
                if (docSnap.Exists)
                {
                    Dictionary<string, object> duikerDict = docSnap.ToDictionary();

                    Duiker d = docSnap.ConvertTo<Duiker>();

                    Timestamp ts = (Timestamp)duikerDict["geboorteDatum"];
                    DateTime gbDatum = ts.ToDateTime().ToLocalTime();
                    d.geboorteDatum = gbDatum;

                    duikers.Add(d);
                }
            }

            return duikers;
        }
    }
}
