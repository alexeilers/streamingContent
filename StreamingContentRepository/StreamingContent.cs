namespace StreamingContentRepo;
public class StreamingContent
{
    public string Title { get; set; }
    public string Description { get; set; }
    public double StarRating { get; set; }
    public string MaturityRating { get; set; }
    public bool IsFamilyFriend { get; set; }
    public bool AlexRecommends { get; set; }
    public GenreType TypeOfGenre { get; set; }

    public StreamingContent(){}
    public StreamingContent(string title, string description, string maturityRating, double starRating, bool isFamilyFriendly, bool alexRecommends){
        Title = title;
        Description = description;
        MaturityRating = maturityRating;
        StarRating = starRating;
        IsFamilyFriend = isFamilyFriendly; 
        AlexRecommends = alexRecommends;
    }
    
}

public enum GenreType {
    Horror,
    Comedy, 
    Drama,
    Gore,
    Mystery,
    Action, 
}