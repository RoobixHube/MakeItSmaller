using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MakeItSmaller.Services
{
    public interface IMISURLService
    {
        bool CreateMISURLFromURL(string URL);
        string GetMISURLFromURL(string URL);
        string GetURLFROMMISURL(string misURL);
    }
}
