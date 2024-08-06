namespace Common.DataAccess.SharedEntities.Users
{
    public class IdentityEntity
    {
        public required Guid Id { get; set; }
        
        public required string Description { get; set; }
        public required string Habits { get; set; }
        public required string PhysicalAttributes { get; set; } 
        
        public Guid PersonaId { get; set; }
        public PersonaEntity? Persona { get; set; }
    }
}
