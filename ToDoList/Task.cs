public class Task
{
    public uint Id { get; set; }
    public DateTime TimeToCreate { get; set; }
    public string Specification { get; set; }
    public byte CompletionStage { get; set; }

    public Task(uint id, string specification)
    {
        Id = id;
        Specification = specification;
        CompletionStage = 0;
        TimeToCreate = DateTime.Now;
    }

    public override string ToString()
    {
        return $" [{(CompletionStage == 2 ? "+" : CompletionStage == 1 ? "-" : " ")}] ({TimeToCreate.Day}/{TimeToCreate.Month}/{TimeToCreate.Year}) {Id}: {Specification}";
    }
}

