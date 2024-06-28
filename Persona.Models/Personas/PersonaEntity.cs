using Persona.Models.Personas.Base;

namespace Persona.Models.Personas
{
    public class PersonaEntity : PersonaBase
    {
        public Guid OwnerId { get; set; }
        public Guid Id { get; set; }
    }
}
