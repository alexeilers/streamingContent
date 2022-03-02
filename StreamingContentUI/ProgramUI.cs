using StreamingContentRepo;
public class ProgramUI {
private readonly StreamingContentRepository _streamingRepo = new StreamingContentRepository();

public void Run(){
    SeedContentList();
    RunMenu();

}

public void RunMenu(){
    bool continueToRun = true;
    while(continueToRun){
        Console.Clear();

        Console.WriteLine("Enter the number of the option you'd like to select:\n" + 
        "1) Show all streaming content\n" + 
        "2) Find streaming content by Title\n" +
        "3) Add new streaming content\n" +
        "4) Remove streaming content\n" +
        "5) Show by Star Rating\n" +
        "6) Show only Family Friendly\n" +
        "7) Recommended by Alex\n" +
        "8) Exit");

        string userInput = Console.ReadLine();

        switch(userInput){
            case "1":
            ShowAllContent();
            break;
            case "2":
            ShowContentByTitle();
            break;
            case "3":
            CreateNewContent();
            break;
            case "4":
            RemoveContentFromList();
            break;
            case "5":
            ShowContentByStarRating();
            break;
            case "6":
            ShowByFamilyFriendly();
            break;
            case "7":
            RecommendedByAlex();
            break;
            case "8":
            continueToRun = false;
            break;
            default:
            Console.WriteLine("Please enter a valid number between 1 and 5.\n" +
            "Press any key to continue...");
            Console.ReadKey();
            break;

        }

    }
}

private void CreateNewContent(){
    Console.Clear();
    
    StreamingContent content = new StreamingContent();
    Console.WriteLine("Please enter a Title");
    content.Title = Console.ReadLine();

    Console.WriteLine("Please enter at brief description of your movie");
    content.Description = Console.ReadLine();

    Console.WriteLine("Please give us a star rating of your movie: ");
    content.StarRating = double.Parse(Console.ReadLine());

    Console.WriteLine("Please select a maturity rating:\n" +
    "1) G\n" +
    "2) PG\n" +
    "3) PG-13\n" +
    "4) R\n" +
    "5) NC-17\n" +
    "6) TV-Y\n" +
    "7) TV-G\n" +
    "8) TV-PG\n" +
    "9) TV-14\n" +
    "10) TV-MA\n");
    string MaturityRating = Console.ReadLine();

    switch (MaturityRating){
        case "1":
        content.MaturityRating = "G";
        break;
        case "2":
        content.MaturityRating = "PG";
        break;
        case "3":
        content.MaturityRating = "PG-13";
        break;
        case "4":
        content.MaturityRating = "R";
        break;
        case "5":
        content.MaturityRating = "NC-17";
        break;
        case "6":
        content.MaturityRating = "TV-Y";
        break;
        case "7":
        content.MaturityRating = "TV-G";
        break;
        case "8":
        content.MaturityRating = "TV-PG";
        break;
        case "9":
        content.MaturityRating = "TV-14";
        break;
        case "10":
        content.MaturityRating = "TV-MA";
        break;

    }

    Console.WriteLine("Select a Genre:\n" +
    "1) Horror\n" +
    "2) Comedy\n" +
    "3) Drama\n" +
    "4) Gore\n" +
    "5) Mystery\n" +
    "6) Action");

    string genreInput = Console.ReadLine();
    int genreID = int.Parse(genreInput);

    content.TypeOfGenre = (GenreType)genreID;

    _streamingRepo.AddContentToDirectory(content);

    }

    private void ShowAllContent(){
        Console.Clear();
        List<StreamingContent> listOfContent = _streamingRepo.GetContents();

        foreach(StreamingContent content in listOfContent) {
            DisplayContent(content);
        }

        Console.WriteLine("Press any key to continue...");
        Console.ReadKey();

    }

private void DisplayContent(StreamingContent content){
    Console.WriteLine($"Title: {content.Title}\n" +
    $"Descritpion: {content.Description}\n" +
    $"Genre: {content.TypeOfGenre}\n" +
    $"Star Rating: {content.StarRating}\n" +
    $"Family Friendly: {content.IsFamilyFriend}\n" +
    $"Maturity Rating: {content.MaturityRating}\n");
}

private void ShowContentByTitle(){
    Console.Clear();
    Console.WriteLine("Enter a Title:");
    string title = Console.ReadLine();
    StreamingContent content = _streamingRepo.GetContentByTitle(title);

    if(content != null){
        DisplayContent(content);
    }
    else Console.WriteLine("No results found.");

    Console.WriteLine("Press any key to continue...");
    Console.ReadKey();

}

private void RemoveContentFromList(){
    Console.Clear();
    Console.WriteLine("Which item would you like to remove?");
    List<StreamingContent> contentList = _streamingRepo.GetContents();
    int count = 0;

    foreach(StreamingContent content in contentList){
        count++;
        Console.WriteLine($"{count}. {content.Title}");
    }

    int targetContentID = int.Parse(Console.ReadLine());
    int targetIndex = targetContentID -1;

    if(targetIndex >= 0 && targetIndex < contentList.Count){
        StreamingContent desiredContent = contentList[targetIndex];

        if(_streamingRepo.DeleteExistingContent(desiredContent)){
            Console.WriteLine($"{desiredContent.Title} was successfully removed.");
        }
        else Console.WriteLine("I'm sorry, Dave. I'm afraid I can't do that.");
    }

    else Console.WriteLine("No content has that ID.");

    Console.WriteLine("Press any key to continue...");
    Console.ReadKey();

}

private void ShowContentByStarRating(){
    Console.Clear();
    List<StreamingContent> listContent = _streamingRepo.GetContents();
    Console.Write("Please enter a star rating from 1 to 5: ");
    string starInput = Console.ReadLine();
    int stars = int.Parse(starInput);

    foreach(StreamingContent content in listContent){

    if(content.StarRating == stars){
        DisplayContent(content); }
    }
    Console.WriteLine("Press any key to continue...");
    Console.ReadKey();
}

private void ShowByFamilyFriendly(){
    Console.Clear();
    List<StreamingContent> listContent = _streamingRepo.GetContents();
    foreach(StreamingContent content in listContent){
    if(content.MaturityRating == "G" || content.MaturityRating == "PG"){
        DisplayContent(content);}
    }
    Console.WriteLine("Press any key to continue...");
    Console.ReadKey();
}

private void RecommendedByAlex(){
    Console.Clear();
    List<StreamingContent> listContent = _streamingRepo.GetContents();
    foreach(StreamingContent content in listContent){
    if(content.AlexRecommends == true){
        DisplayContent(content);}
    }
    Console.WriteLine("Press any key to continue...");
    Console.ReadKey();
}



private void SeedContentList(){
    StreamingContent rubber =  new StreamingContent("Rubber","Tire comes to life and kills people", "R", 2, false, true);
    StreamingContent toyStory = new StreamingContent("Toy Story", "Best childhood movie.","PG", 4, true, true);
    StreamingContent starWars = new StreamingContent("Star Wars", "Lightsabers is all you need to know", "PG-13", 2, true, false);
    StreamingContent airBud = new StreamingContent("Air Bud", "Dogs and Soccer", "PG", 4, true,true);
    StreamingContent theNotebook = new StreamingContent("The Notebook", "Dementia and stuff", "R", 3, true, false);
    StreamingContent cats = new StreamingContent("Cats: The Musical", "Even if you like cats, steer clear at all costs.", "PG-13", 1, true, false);
    StreamingContent spinalTap = new StreamingContent("Spinal Tap", "Hello, Cleveland!!!", "R", 5, false, true);
    StreamingContent tonyDallas = new StreamingContent("Tony Does Dallas", "A new spin on an old classic!", "NC-17", 5, false, true);

    _streamingRepo.AddContentToDirectory(rubber);
    _streamingRepo.AddContentToDirectory(toyStory);
    _streamingRepo.AddContentToDirectory(starWars);
    _streamingRepo.AddContentToDirectory(airBud);
    _streamingRepo.AddContentToDirectory(theNotebook);
    _streamingRepo.AddContentToDirectory(cats);
    _streamingRepo.AddContentToDirectory(spinalTap);
    _streamingRepo.AddContentToDirectory(tonyDallas);

}
}
