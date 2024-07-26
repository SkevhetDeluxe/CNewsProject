using Microsoft.AspNetCore.Mvc.Rendering;
using CNewsProject.Models.Database;

namespace CNewsProject.Services
{
    public class CNewsService : ICNewsService
    {
        private readonly ArticleDbContext _db;
        private readonly IConfiguration _configuration;

        public CNewsService(CNewsDbContext db, IConfiguration configuration)
        {
            _db = db;
            _configuration = configuration;
        }

        #region Base_Methods()
        public List<Movie> GetAllMovies()
        {
            return _db.Movies.OrderBy(m => m.Title).ToList();
        }

        public Movie GetMovieById(int Id)
        {
            return _db.Movies.FirstOrDefault(m => m.Id == Id);
        }

        public void AddMovie(Movie movie)
        {
            _db.Movies.Add(movie);
            _db.SaveChanges();
        }

        public void RemoveMovie(Movie movie)
        {
            _db.Movies.Remove(_db.Movies.FirstOrDefault(m => m.Id == movie.Id));
            _db.SaveChanges();
        }




        public void EditMovie(Movie movie)
        {
            GetMovieById(movie.Id).Title = movie.Title;
            GetMovieById(movie.Id).Category = movie.Category;
            GetMovieById(movie.Id).Director = movie.Director;
            GetMovieById(movie.Id).Language = movie.Language;
            GetMovieById(movie.Id).Country = movie.Country;
            GetMovieById(movie.Id).ReleaseYear = movie.ReleaseYear;
            GetMovieById(movie.Id).Price = movie.Price;
            GetMovieById(movie.Id).ImageURL = movie.ImageURL;





            _db.SaveChanges();
        }



        #endregion


        #region Get_Lists_With_Filters()
        // Overload later to take filters and sortings
        public List<Movie> GetMovieList()
        {
            List<Movie> movieList = GetAllMovies();

            return movieList;
        }

        public List<Movie> GetMovieList(MovieListVM vModel)
        {
            List<Movie> movieList = GetAllMovies();

            if (vModel.Director != "All")
                movieList = movieList.Where(m => m.Director == vModel.Director).ToList();

            if (vModel.Category != "All")
                movieList = movieList.Where(m => m.Category == vModel.Category).ToList();

            return movieList;
        }


        // Get SORTINGS and FILTERS
        public List<string> GetDirectors() // Gets a List<string> of Directors. No Dupes.
        {
            List<string> directors = new()
            {
                "All"
            };

            foreach (Movie movie in GetAllMovies())
            {
                if (!directors.Contains(movie.Director))
                    directors.Add(movie.Director);
            }

            return directors;
        }

        public List<string> GetCategories() // Gets a List<string> of Categories. No Dupes.
        {
            List<string> categories = new()
            {
                "All"
            };

            foreach (Movie movie in GetAllMovies())
            {
                if (!categories.Contains(movie.Category))
                    categories.Add(movie.Category);
            }

            return categories;
        }

        // SELECT LISTS
        public List<SelectListItem> GetDirectorList()
        {
            List<SelectListItem> directorList = new();

            foreach (string director in GetDirectors())
            {
                directorList.Add(new SelectListItem
                {
                    Value = director,
                    Text = director
                });
            }

            return directorList;
        }

        public List<SelectListItem> GetCategoryList()
        {
            List<SelectListItem> categoryList = new();

            foreach (string category in GetCategories())
            {
                categoryList.Add(new SelectListItem
                {
                    Value = category,
                    Text = category
                });
            }

            return categoryList;
        }
        #endregion


        #region Specifics_For_FrontPage()
        // These Methods are for displaying TOP Fives on the front page

        public List<Movie> FPMostPopular()



        {
            var fpmostpopular = _db.OrderRows.GroupBy(or => or.MovieId)
               .OrderByDescending(g => g.Count())
               .Select(m => new Movie()
               {
                   Id = m.Key,
                   ImageURL = m.First().Movie.ImageURL,
                   Title = m.First().Movie.Title,
                   Price = m.First().Movie.Price,

               }).Take(5).ToList();

            return fpmostpopular;



        }


        public List<Movie> FPNewest()
        {
            List<Movie> movieList = new();
            movieList = GetAllMovies().OrderByDescending(m => m.ReleaseYear)
                .Take(5).ToList();

            return movieList;
        }
        public List<Movie> FPOldest()
        {
            List<Movie> movieList = new();
            movieList = GetAllMovies().OrderBy(m => m.ReleaseYear)
                .Take(5).ToList();

            return movieList;
        }
        public List<Movie> FPCheapest()
        {
            List<Movie> movieList = new();
            movieList = GetAllMovies().OrderBy(m => m.Price)
                .Take(5).ToList();

            return movieList;
        }

        #region SetupVM()_For_FrontPage_Specifics

        // Since FrontPageVM carries a List<List<Movie>> it NEEDS to be
        // filled and displayed in the same order EVERY time or you will 
        // get the wrong lists on the wrong places.

        /* Order is
         * 1. Popular
         * 2. Newest
         * 3. Oldest
         * 4. Cheapest
         */

        public FrontPageVM SetupFPVM()
        {
            FrontPageVM vModel = new();
            vModel.FrontPageMovieLists = new()
            {
                { FPMostPopular() },
                { FPNewest() },
                { FPOldest() },
                { FPCheapest() }
            };

            return vModel;
        }

        #endregion


        #endregion

        #region Setups_And_UpdateVM()
        // UPDATE METHODS, Used for Setting and Updating
        public MovieListVM UpdateVM()
        {
            MovieListVM vModel = new();

            vModel.CategoryList = GetCategoryList();
            vModel.DirectorList = GetDirectorList();
            vModel.MovieList = GetMovieList();

            return vModel;
        }

        public MovieListVM UpdateVM(MovieListVM vModel)
        {
            vModel.CategoryList = GetCategoryList();
            vModel.DirectorList = GetDirectorList();
            vModel.MovieList = GetMovieList(vModel);

            return vModel;
        }

        public MovieListVM UpdateVM(int pageNum, int pageSize)
        {
            MovieListVM vModel = new();
            vModel.SelectedPageSize = Convert.ToString(pageSize);

            vModel.CategoryList = GetCategoryList();
            vModel.DirectorList = GetDirectorList();
            vModel.MovieList = GetMovieList()
                .Skip((pageNum - 1) * pageSize).Take(pageSize).ToList();

            vModel.PageNum = pageNum;
            vModel.PageSize = pageSize;

            if (pageSize < GetAllMovies().Count())
            {
                vModel.MaxPage = (GetAllMovies().Count() % pageSize == 0)
                    ? GetAllMovies().Count() / pageSize : GetAllMovies().Count() / pageSize + 1;
            }
            else
                vModel.MaxPage = 1;

            return vModel;
        }

        public MovieListVM UpdateVM(int pageNum, int pageSize, MovieListVM vModel)
        {
            vModel.SelectedPageSize = Convert.ToString(pageSize);

            vModel.CategoryList = GetCategoryList();
            vModel.DirectorList = GetDirectorList();
            vModel.MovieList = GetMovieList(vModel)
                .Skip((pageNum - 1) * pageSize).Take(pageSize).ToList();

            vModel.PageNum = pageNum;
            vModel.PageSize = pageSize;

            if (pageSize < GetAllMovies().Count())
            {
                vModel.MaxPage = (GetAllMovies().Count() % pageSize == 0)
                    ? GetAllMovies().Count() / pageSize : GetAllMovies().Count() / pageSize + 1;
            }
            else
                vModel.MaxPage = 1;

            return vModel;
        }

        #endregion
    }
}
