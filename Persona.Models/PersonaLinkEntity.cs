namespace Persona.Models;

public class PersonaLinkEntity
{
    public Guid UserId { get; set; }
    public Guid ConversationId { get; set; }
    public Guid PersonaId { get; set; }
}
