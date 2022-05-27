

namespace Movies.Models
{
    using Newtonsoft.Json;
public class Movie

{
    [JsonProperty(PropertyName = "id")]
    public string Id { get; set; }

    [JsonProperty(PropertyName = "userId")]
    public string UserId { get; set; }

    [JsonProperty(PropertyName = "title")]
    public string Title { get; set; }

    [JsonProperty(PropertyName = "description")]
    public string Description { get; set; }
    
    [JsonProperty(PropertyName = "poster_img")]
    public string Poster_image{ get; set; }
    
    [JsonProperty(PropertyName = "vote_avg")]
    public float Vote_average { get; set; }
    
    [JsonProperty(PropertyName = "release_date")]
    public string Release_date { get; set; }
    
    
}

    
}