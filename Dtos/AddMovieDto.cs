using Newtonsoft.Json;

namespace Movies.Dtos;

public class AddMovieDto
{
    [JsonProperty(PropertyName = "id")]
    public string Id { get; set; }
    

    [JsonProperty(PropertyName = "title")]
    public string Title { get; set; }

    [JsonProperty(PropertyName = "description")]
    public string Description { get; set; }
    
    [JsonProperty(PropertyName = "poster_img")]
    public string Poster_img{ get; set; }
    
    [JsonProperty(PropertyName = "vote_avg")]
    public float Vote_avg { get; set; }
    
    [JsonProperty(PropertyName = "release_date")]
    public string Release_date { get; set; }
}