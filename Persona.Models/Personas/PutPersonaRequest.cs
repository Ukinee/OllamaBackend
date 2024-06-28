using Persona.Models.Personas.Base;

namespace Persona.Models.Personas
{
    public class PutPersonaRequest : PersonaBase
    {
        public Guid Id { get; set; }
    }
}
