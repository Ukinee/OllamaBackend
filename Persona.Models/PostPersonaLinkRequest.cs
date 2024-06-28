namespace Persona.Models
{
    public class PostPersonaLinkRequest
    {
        public Guid PersonaId { get; set; }
        public Guid ConversationId { get; set; }
    }
}
