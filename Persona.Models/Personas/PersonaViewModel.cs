namespace Persona.Models.Personas
{
    public class PersonaViewModel
    {
        public required Guid Id { get; set; }
        public required string Name { get; set; }
        
        public required ICollection<Guid> ConversationIds { get; set; }
    }
}
