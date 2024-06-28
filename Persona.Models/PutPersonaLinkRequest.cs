namespace Persona.Models
{
    public class PutPersonaLinkRequest
    {
        public Guid PersonaId { get; set; }
        public Guid ConversationId { get; set; }
    }
}
