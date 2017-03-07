using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogTest
{
    class Program
    {
        static void Main(string[] args)
        {

            Console.WriteLine("1.Create Blog\n2.CreatePost\n3.List All blogs\n4.List specific blog post\n0.End\nPlease enter your choice:");
            int choice = int.Parse(Console.ReadLine());
            Console.Clear();
            while (choice != 0)
            {
                switch (choice)
                {
                    case 1:
                        CreateBlog();
                        break;
                    case 2:
                        CreatePost();
                        break;
                    case 3:
                        ListAllBlogs();
                        break;
                    case 4:
                        ListSpecificBlog();
                        break;
                }
                Console.WriteLine(
                    "1.Create Blog\n2.CreatePost\n3.List All blogs\n4.List specific Blog Post\n0.End\nPlease enter your choice:");
                choice = int.Parse(Console.ReadLine());
                Console.Clear();
            }
            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();

            
        }

        private static void CreateBlog()
        {
            using(var db = new BloggingContext())
            {
                Console.WriteLine("Enter your User name: ");
                var username = Console.ReadLine();
                Console.WriteLine("Enter a name for a new Blog: ");
                var name = Console.ReadLine();
                Console.WriteLine("Enter an URL for the new Blog: ");
                var url = Console.ReadLine();
                var blog = new Blogs { UserName = username, Name = name, Url = url };

                db.Blogs.Add(blog);
                db.SaveChanges();  
            }

            Console.WriteLine("Press any key to exit");

            Console.ReadKey();

        }

        private static void ListAllBlogs()
        {
            using(var db = new BloggingContext())
            {
                var query =
                    from b in db.Blogs
                    orderby b.Name
                    select b;

                Console.WriteLine("All Blogs in database:");
                foreach (var item in query)
                {
                    Console.WriteLine(item.ToString());
                }
            }
        }

        private static void CreatePost()
        {
            using(var db = new BloggingContext())
            {
                Console.WriteLine("Enter your Post's Title: ");
                var postTitle = Console.ReadLine();

                Console.WriteLine("Enter your Post's Content: ");
                var postContent = Console.ReadLine();

                Console.WriteLine("Enter your Blog Id: ");
                var blogId = Convert.ToInt32(Console.ReadLine());

                var post = new Posts { Title = postTitle, Content = postContent, BlogId = blogId };
                db.Posts.Add(post);
                db.SaveChanges();

            }
        }

        private static void ListSpecificBlog()
        {
            Console.WriteLine("Hvilken blog:");
            var blogname = Console.ReadLine();

            using(var db = new BloggingContext())
            {
                var b1 = from b in db.Blogs
                         where b.Name.Contains(blogname)
                         select b;

                var p1 = from p in db.Posts
                         join b11 in b1
                         on p.BlogId equals b11.BlogId
                         select p;

                foreach (var blog in b1)
                {
                    Console.WriteLine($"Bloggen er: {blog.ToString()}");
                    foreach (var post in p1)
                    {
                        Console.WriteLine(post);
                    }
                }
               
            }
        }
    }
}
