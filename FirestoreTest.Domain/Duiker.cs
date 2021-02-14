using System;
using System.Collections.Generic;
using Google.Cloud.Firestore;

namespace FirestoreTest.Domain
{
    [FirestoreData]
    public class Duiker
    {
        [FirestoreProperty]
        public string adres { get; set; }
        [FirestoreProperty]
        public IEnumerable<string> nummerplaten { get; set; }
        [FirestoreProperty]
        public int id { get; set; }
        [FirestoreProperty]
        public string email { get; set; }
        [FirestoreProperty]
        public DateTime geboorteDatum { get; set; }
        [FirestoreProperty]
        public string gemeente { get; set; }
        [FirestoreProperty]
        public bool heeftBeperking { get; set; }
        [FirestoreProperty]
        public bool isBegeleider { get; set; }
        [FirestoreProperty]
        public string naam { get; set; }
        [FirestoreProperty]
        public int postcode { get; set; }
        [FirestoreProperty]
        public string gsm { get; set; }
    }
}
