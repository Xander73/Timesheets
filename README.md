Timesheets программа расчета времени выполнения задачи. В программе предусмотрена аутентификация и авторизация. 
Мтоды Authenticate, Refresh, GetUsers и PutUser не требуют авторизации. 
Аргументы типов string и int имеют механизм валидации.

В проекте Timesheets.DB 
    В контроллерах UsersController и EmployeesController
         В методах Authenticate аргументы user и password должны иметь длинну не меньше 3 и не больше 50 символов.
         В методах SetTokenCookie длинна токена ограничена размерами 150-200 символов.
    В репозитории IBaseRepo 
        В методе GetByTerm аргумент term должен иметь длинну от 3 до 50 символов.
        В методе GetSomePersons аргумент skip должен иметь значения  от 0 до int.MaxValue и аргумент take - от 1 до int.MaxValue.
    В IUserService 
        В методах Authenticate аргументы user и password должны иметь длинну не меньше 3 и не больше 50 символов.
         В методах SetTokenCookie длинна токена ограничена размерами 150-200 символов.
