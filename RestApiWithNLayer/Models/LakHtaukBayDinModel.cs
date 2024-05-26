namespace KZHDotNetCore.RestApiWithNLayer.Models;

public class LatHtaukBayDinModel
{
    public QuestionModel[] questions { get; set; }
    public AnswerModel[] answers { get; set; }
    public string[] numberList { get; set; }
}
