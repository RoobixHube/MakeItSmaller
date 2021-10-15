using MakeItSmaller.Models;

namespace MakeItSmaller.DataLayer
{
    public interface IMISURLRepository
    {
        bool CreateNewMISURLPairing(MISURLCouple misURL);
        string GetMISURLFromURL(string URL);
        string GetURLFROMMISURL(string misURL);
    }
}
