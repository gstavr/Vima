using Vimachem.Models.Domain;

namespace Vimachem.Data
{
    public static class CategoryStore
    {
        public static List<Category> categoryList =
        [
            new () {Id = 1, Description = "This category encompasses a broad range of books that are primarily created from the imagination of the author. Fiction books may include novels, short stories, and novellas. Genres within fiction can vary widely, encompassing everything from historical fiction and fantasy to mystery, science fiction, and contemporary literature.", CreatedDate = new DateTime(), Name = "Fiction"},
            new () {Id = 2, Description = "Non-fiction books are based on real facts and truthful accounts of events and ideas. This category includes biographies, memoirs, essays, and self-help books. Non-fiction also covers educational subjects such as history, psychology, science, and business.", CreatedDate = new DateTime(), Name = "Non-Fiction"},
            new() { Id = 3, Description = "Books in this category are known for suspenseful plots that involve investigations and the solving of crimes. They often revolve around a mysterious event or a crime that needs to be solved, typically leading to a climactic reveal or twist.", CreatedDate = new DateTime(), Name = "Mystery & Thriller" },
            new() { Id = 4, Description = "This category includes books that explore imaginative and futuristic concepts, alternative worlds, and advanced technology. Science fiction often deals with themes like space exploration and time travel, while fantasy books may include elements such as magic, mythical creatures, and medieval settings.", CreatedDate = new DateTime(), Name = "Science Fiction & Fantasy" },
            new() { Id = 5, Description =  "Romance books explore the theme of love and relationships between people. These stories often focus on romantic relationships from the courtship to the culmination of a love story, providing emotional narratives that may also delve into the characters’ personal growth.", CreatedDate = new DateTime(), Name = "Romance" },
            new() { Id = 6,Description = "Designed to appeal to children from infancy through elementary school, these books range from picture books to easy readers and early chapter books. They often teach lessons through fun stories and vibrant illustrations, helping children understand their world and sparking their imagination.", CreatedDate = new DateTime(), Name = "Children’s Books" },
        ];
    }
}
