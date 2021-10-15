using MakeItSmaller.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web;

namespace MakeItSmaller.DataLayer
{
    public class MISURLRepository : IMISURLRepository
    {
        private readonly string _dbFilename;

        public MISURLRepository(string dbFileName)
        {
            if (string.IsNullOrWhiteSpace(dbFileName))
            {
                throw new ArgumentException($"'{nameof(dbFileName)}' cannot be null or whitespace.", nameof(dbFileName));
            }

            if (!File.Exists(dbFileName))
            {
                var dbFile = File.Create(dbFileName);
                dbFile.Close();
            }

            _dbFilename = dbFileName;
        }

        public bool CreateNewMISURLPairing(MISURLCouple misURL)
        {
            try
            {
                using (StreamWriter w = File.AppendText(_dbFilename))
                {
                    w.WriteLine($"{misURL.URL},{misURL.MISURL}");
                    w.Close();
                }
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Db Writing Error: {ex.Message}, {ex.InnerException}");
                return false;
            }
        }

        public string GetMISURLFromURL(string URL)
        {
            var misURLList = GetDataFromFile();

            if (misURLList == null)
            {
                return null;
            }

            var foundMISURL = misURLList.Where(u => u.URL == URL).FirstOrDefault();

            return foundMISURL == null ? null : foundMISURL.MISURL;
        }

        public string GetURLFROMMISURL(string misURL)
        {
            var misURLList = GetDataFromFile();

            if (misURLList == null)
            {
                return null;
            }

            var foundMISURL = misURLList.Where(u => u.MISURL == misURL).FirstOrDefault();

            return foundMISURL == null ? null : foundMISURL.URL;
        }

        private List<MISURLCouple> GetDataFromFile()
        {
            var MISURLList = new List<MISURLCouple>();
            using (StreamReader reader = new StreamReader(_dbFilename))
            {
                string dataLine;
                while ((dataLine = reader.ReadLine()) != null)
                {
                    var misURL = GenerateMISURLFromFileData(dataLine);

                    MISURLList.Add(misURL);
                }

                reader.Close();
            }

            if (MISURLList == null || !MISURLList.Any())
            {
                return null;
            }

            return MISURLList;
        }

        private MISURLCouple GenerateMISURLFromFileData(string dataLine)
        {
            var urlSplit = dataLine.Split(',');
            return new MISURLCouple()
            {
                URL = urlSplit[0],
                MISURL =urlSplit[1]
            };
        }
    }
}