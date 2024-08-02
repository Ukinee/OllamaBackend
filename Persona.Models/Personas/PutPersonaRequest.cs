namespace Persona.Models.Personas
{
    public class PutPersonaRequest
    {
        public required Guid Id { get; set; }
        public required string Name { get; set; }
    }
}
