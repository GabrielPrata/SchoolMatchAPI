namespace SearchService.Data.DTO.Profile
{
    public class BlocksDTO
    {
        public BlocksDTO(int blockId, string blockName)
        {
            BlockId = blockId;
            BlockName = blockName;
        }

        public BlocksDTO() { }

        public int BlockId { get; set; }
        public string BlockName { get; set; }
    }
}
