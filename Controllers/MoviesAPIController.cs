using System.Net;
using System.Text;
using Microsoft.AspNetCore.Mvc;

namespace Movies.Controllers;

public class MoviesAPIController : Controller
{
    [HttpGet("GetObjFromApi")]
    public string GetObjFromApi()
    {
        HttpWebRequest request = (HttpWebRequest)WebRequest.Create(
            "https://api.themoviedb.org/3/discover/movie?api_key=c0d2b5193ae0ea4f12cccda56ceab5c1&with_genres=18&sort_by=vote_average.desc&vote_count.gte=10");
        request.Method = "GET";
        request.ContentType = "application/json:charset=UTF-8";

        HttpWebResponse response = (HttpWebResponse)request.GetResponse();
        Stream stream = response.GetResponseStream();
        StreamReader reader = new StreamReader(stream, Encoding.GetEncoding("utf-8"));

        string json = reader.ReadToEnd();

        reader.Close();
        stream.Close();
        response.Close();


        return json;

    }
}