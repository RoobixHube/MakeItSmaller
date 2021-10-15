using MakeItSmaller.DataLayer;
using MakeItSmaller.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MakeItSmaller.Services
{
    public class MISURLService : IMISURLService
    {
        private readonly IMISURLRepository _repository;
        private static Random random = new Random();
        const string allowedChars = "abcdefghifklmnopqrstuvwxyz1234567890";
        const int allowedLength = 10;

        public MISURLService(IMISURLRepository repository)
        {
            this._repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        public bool CreateMISURLFromURL(string URL)
        {
            var existingURL = _repository.GetMISURLFromURL(URL);

            if (string.IsNullOrWhiteSpace(existingURL))
            {
                var newMISURL = new MISURLCouple() 
                {
                    URL = URL,
                    MISURL= GenerateMSIString()
                };

                return _repository.CreateNewMISURLPairing(newMISURL);
            }

            return true;
        }

        private string GenerateMSIString()
        {
            return new string(Enumerable.Repeat(allowedChars, allowedLength)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        public string GetMISURLFromURL(string URL)
        {
            return _repository.GetMISURLFromURL(URL);
        }

        public string GetURLFROMMISURL(string misURL)
        {
            return _repository.GetURLFROMMISURL(misURL);
        }
    }
}