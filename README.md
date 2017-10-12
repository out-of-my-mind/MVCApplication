**HTML辅助方法默认编码**  
@Html.TextBox("Title",Model.Title)  
<input id="Title" name="Title" type="text" value="This is Model.Title Context"/>  

@Html.TextArea("text","<hello <br /> world")  
<textarea cols="20" id="text" name="text" rows="2">hello &lt;br /&gt; world</textarea>  
@Html.TextArea("text","hello <br /> world",10,80,null)  
<textarea clos="80" id="text" name="text" rows="10">hello &lt;br /&gt; world</textarea>  

@Html.Label("GenreId")  
<label for="GenreId">Genre</label>  
label标签的作用为其他输入元素显示附加信息。for属性是其他输入元素的ID。  

@Html.Hidden("wizardStep","1")  
会生成如下代码  
<input id="wizardStep" name="wizardStep" type="hidden" value="1" />  
强辅助方法为@Html.HiddenFor(m => m.WizardStep)  

@Html.Password("UserPassword")  
会生成如下代码  
<input id="UserPassword" name="UserPassword" type="password" value="" />  

@Html.RadioButton("color","red")  
@Html.RadioButton("color","blue",true)  
@Html.RadioButton("color","green")  
生成如下  
<input id="color" name="color" type="radio" value="red" />  
<input id="color" name="color" type="radio" value="blue" checked="checked" />  
<input id="color" name="color" type="radio" value="green" />  
对应的强类型方法如下  
@Html.RadioButtonFor(m => m.color,"red")  
@Html.RadioButtonFor(m => m.color,"blue",true)  
@Html.RadioButtonFor(m => m.color,"green",true)  

@Html.CheckBox("IsDiscounted")  
生成如下代码  
<input id="IsDiscounted" name="IsDiscounted" type="checkbox" value="true" />  
<input name="IsDiscounted" type="hidden" value="false" />  
渲染两个输入元素是因为，HTML规范中规定浏览器只会提交"开"(选中的复选框的值)，隐藏的输入元素保证了IsDiscounted有一个值会被提交，即使用户没有选择这个复选框  

---超链接-------------------------  
ActionLink辅助方法能够渲染一个超链接，可以指向另一个控制器操作。该方法在后台使用路由API来生成URL  
@Html.ActionLink("Link Text","AnotherAction")  
假设是在Home路径中，会生成如下HTML  
<a href="/home/AnotherAction">LinkText</a>  
当需要只向不同的控制操作的链接时，通过第三个参数指向。例如链接到ShoppingCartController控制器的Index操作  
@Html.ActionLink("Link Text","Index","ShoppingCart")  
当需要传递参数的时候，可以使用以下两种方法  
@Html.ActionLink("Link Text","Edit","StoreManager",new {id=123},null)//最后一个参数是HTML元素属性(必带)  
@Html.RouteLink("Link Text",new {action="AnotherAction"})//只可以接受路由名称，而不能接收控制器名称和操作名称。这种写法和@Html.ActionLink("Linkl Text","AnotherAction")效果一样  

URL辅助方法是以字符串的形式返回URL。有三种辅助方法：Action、Content、RouteUrl  
@Url.Action("Browse","Store",new {genre="Jazz"},null)  
生成如下HTML标记  
/Store/Browse?genre=Jazz  
@Url.Content可以把相对路径转换成绝对路径  
<script src="~/Script/jqurey-1.10.2.min.js" type="text/javascripr"></script>  
---end------------------------  

Partial辅助方法用于将视图渲染成字符串。(不需要指定路径、文件名)  
@Html.partial("AlbumDisplay")  
RenderPartial辅助方法是直接写入响应输出流，出于这个原因必须把RenderPartial放入代码块中，而不能放入代码表达式中。下面两行向输出流写入相同的内容  
@{Html.RenderPartial("AlbumDisplay");}//性能较好，因为直接写入响应流，这种优势要在高流量中明显  
@Html.Partial("AlbumDisplay")  

Action辅助方法执行单独的控制器操作显示结果，Partial辅助方法通常在单独的文件中应用视图标记来渲染。  
[ChildActionOnly]//这个特性避免了通过URL来调用操作。相反只能通过Action和RenderAction使用  
public ActionResult Menu(int id){  
    var menu = GetMenuFormSomewhere();  
    return PartialView(menu);  
}  
--------部分视图如下  
@model Menu  
<ul>  
@foreach(var item in Model.MenuItem){  
    <li>@item.Text</li>  
}  
</ul>  
-----------调用控制器操作如下//调用操作来显示内容  
<html>  
<head><title></title></head>  
<body>   
    @Html.Action("Menu")  
</body>  
</html>  
当通过Action或RenderAction调用操作时ControllerContext上的属性IsChildAction值就为true，通过URL调用值为false  
@Html.Action("Menu",new { options = new MenuOption{ width=400,higth=500}})//传递了一个MenuOption对象  

RenderAction方法优先使用ActionName特性值作为调用的操作，下面当调用RenderAction方法是要确保操作名称使用的是CoolMenu  
[ActionName("CoolMenu")]  
public ActionResult Menu()   
{  
    return PartialView();  
}  
---end-----------------  

public ActionResult Edit(int id)  
{  
    var album = db.Albums.Single(a => a.AlbumId == id);  
    ViewBag.Genres = new SelectList(db.Genres.OrderBy(g => g.Name),"GenreId","Name",  album.GenreId);  
    return View(album);  
}  
如果要避免反射开销的同时要自己生成SelectListItem集合，可以使用LINQ的Select方法将SelectListItem对象放入项目Genres中  
public ActionResult Enid(int id)  
{  
    var album = db.Albums.Single(a => a.AlbumId == id);  
    ViewBag.Genres = db.Genres.OrderBy(g => g.Name).AsEnumerable().Select(g => new SelectListItem{  
        Text = g.Name,  
        Value = g.GenreId.ToString(),   
        Selected = album.GenreId == g.GenreId  
    });  
    return View(album);  
}  
  
[httpPost]  
public ActionResult Edit(int id,FormCollection collection)  
{  
    var album = db.Albums.Find(id);  
    ModelState.AddModelError("Title","What a terrible name!");
    return View(album);  
}  
在视图中可以用下行代码显示错误提示消息  
@Html.ValidationMessage("Title")  
生成的HTML标记如下：  
<span class="field-validation-error" data-valmsg-for="Title" data-valmsg-replace="false">
    What a terrible name!  
</span>  

public ActionResult Edit(int id)
{
    ViewBag.Price = 10.0;// new Album{Price = 11};
    return View();
}  
在相应的视图中，使用ViewBag中的值来辅助TextBox:  
@Html.TextBox("Price"); //@Html.TextBox("Album.Price")  辅助方法只能查看ViewData的对象属性  
生成的HTML如下  
<input id="Price" name="Price" type="text" value="10" />//<input id="Album_Price" name="Album.Price" type="text" value="11"/> 如果在ViewData中没有匹配到"Album.Price"，那么将尝试的查找Album类型的对象，再找到对应的属性Price  
在Id特性中包含点"."是非法的，运行时用静态方法HtmlHelper.IdAttributeDotReplacement的值代替了点  

TextBox辅助方法依靠强类型试图数据  
public ActionResult Edit(int id)  
{  
    var ablum = new Album{Price = 12.0m};  
    return View(album);  
} 
在视图中，可以直接使用方法提供的属性来显示信息
@Html.textBox("Price");
生成的HTML标记如下
<input id="Price" name="Price" type="text" value="12.0" />

@Html.TextBox("Title",Model.Title);//辅助方法的第二个参数显式地提供了数据。这是为了避免与ViewBag.Title混淆。一般为了数据清晰，会在数据项前添加前缀。例如ViewBag.Page_Title

除了使用字符串字面值从视图数据中提取值，还可以使用强类型辅助方法。使用强类型方法时需要提供lambda表达式来指定模型属性
@model MvcMusicStore.Model.Album
@using (Html.BeginForm())
{
    @Html.ValidationSummary(excludePropertyErrors: true)
    <fieldest>
        <legend>Edit Album</legend>
        <p>
            @Html.LabelFor(m => m.GenreId)
            @Html.DropDownListFor(m => m.GenreId,ViewBag.Genres as SelectList)
        </p>
        <p>
            @Html.TextBixFor(m => m.Title)//没有显式地设置值，是因为lambda表达式提供了足够的信息，使其能直接读取模型的Title值
            @Html.ValidationMessageFor(m => m.Title)
        </p>
        <input type="submit" value="Save"/>
    </fieldest>
}
强类型的辅助方法名称除了有"For"后缀，跟普通的辅助方法有相同的名称。生成同样的HTML标记
使用强类型的好处包括，智能感知、编译时检查和方便代码重构（如果在模型中改变属性的名称，Visual Studio会自动修改视图中对应的代码）

辅助方法不仅能查看ViewData内部数据，也能得到模型的元数据
@Html.Label("GenreId")
<label for="GenreId">Genre</label>
Genre文本是当辅助方法询问运行时是否有GenreId的可用模型元数据时，从装饰模型的DisplayName特性中获取的信息。
[DisplayName("Genre")]
public inr GenreId{ set; get;}
在辅助方法构建HTML时要用到注解提供的元数据

ASP.NET MVC中的模板辅助方法利用"元数据"和"模板"构建HTML。
其中元数据包括关于模板值（它的名称和类型）的信息和（通过数据注解或自定义提供器添加的）模型元数据。
模板辅助方法有Html.Display和Html.Editor，以及对应的强类型Html.DisplayFor、Html.EditorFor，对应的完整模型Html.DisplayForModel、Html.EditorForModel。
@Html.EdirorFor(m => m.Title)
生成的HTML标记如下
<input id="Title" name="Title" type="text" value="For Those About" />
使用Html.TextBoxFor和Html.EditorFor生成的是同样HTML标记，但是EditorFor方法可以通过数据注解来改变生成的HTML
当使用模板辅助方法时，运行时可以生成合适的任何"编辑器"，如下
[Required(ErrorMessage = "An Album Title is required")]
[StringLength(160)]
[DataType(DataType.MultilineText)]
public string Title{ set; get;}
生成的HTML标记如下
<textarea class="text-box multi-line" id="Title" name="Title">Let There Be Rock</textarea>
因为是在一般意义上请求一个编辑器，所以EditorFor辅助方法首先查看元数据，然后推断应使用的HTML元素

用来显示表单值的所有辅助方法需要和ModelState交互。ModelState是模型绑定的副产品，并且存储模型绑定期间检测到的所有验证错误。辅助方法使用名称表达式作为键，在ModelState字典中查找，如果存在就用ModelaState中的值替换试图数据。
模型绑定失败后，ModelState允许保存”坏“值。为了让用户修改验证错误而重新渲染指定错误的CSS类。

默认情况下，ASP.NET MVC对约定的依赖性很强，避免了一些配置。这些默认项在需要的时候可以被覆盖
Controllers、Models、Views决定在配置文件中，这避免了编辑web.config来显示地告诉MVC引擎在哪里寻找文件
每个控制器名以Controller结尾、所有视图放在单独的Views文件夹、控制器使用的视图名是根据控制器名决定的、所有重用的放在一个共享文件夹中

---验证注解--------
数据注解特性在名称空间System.ComponentModel.DataAnnotation中。支持服务器端验证也支持客户端验证
Required//必需的，非空
[Required]
public string Firstname{ get;set;}
即使用户在浏览器中没有设置javascript的执行权限，验证逻辑也会在服务器端捕获到验证，并反应在用户页面。基于特性的验证，保持了客户端-服务器同步验证规则。

StringLength//数据长度限制，一个汉字算一个长度
[StringLength(160,MinimumLength=3)]//至少3个字符最多160个字符
public string FirstName{ get;set;}

RegularExpression//正则   380687541   @   qq           .  com
[RegularExpression(@"[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-za-z]{2,4}")]
public string Email{ get;set;}

Range特性用来指定数值类型的最大和最小值
[Range(35,44)]
public int Age{ get;set;}
另一个重载版本是
[Range(typeof(decimal),"0.00","49.99")]
public decimal Price{ get;set;}

Compare特性确保模型对象的两个属性拥有相同的值
[RegularExpression(@"[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}")]
public string Email{ get;set;}
[Compare("Email")]
public string EmailConfirm{ get;set;}

Remote
Remote特性可以利用服务器端的回调函数执行客户端的验证逻辑。
[Remote("CheckUserName","Account")]
public string UserName{ get;set;}
在特性中可以设置客户端要调用的控制器名称和操作名称。客户端代码会自动把数据发送到服务器。
public JsonResult CheckUserName(string username)
{
    var result = Membership.FindUsersByName(username).Count == 0;
    return Json(result,JsonRequestBehavior.AllowGet);
}

自定义错误文字信息  
[RegularExpression(@"[A-Za-z0-9._+-]+@[A-Za-z0-9._]+\.[A-Za-z]{2,4}",ErrorMessage="Email doesn't look like a valid email address.")]  
public string Email{ get;set;}  
ErrorMessage是每个验证特性中用来自定义错误信息的参数名称  
[Required(ErrorMessage="Your last name is required")]  
此外还可以使用格式项{0}的格式:  
[Required(ErrorMessage="Your {0} is required"]//格式项会自动填充元数据属性名LastName  
public string LastName{ get;set;}  
假设应用程序是面向国际市场开发，错误提示需要为不同的地区显示不同的文本内容。这时候就需要提供本地化的错误提示指定资源类型名称和资源名称  
[Required(ErrorMessageResourceType=typeof(ErrorMessages),ErrorMessageResourceName="LastNameRequired")]  
上面代码假设有一个ErrorMessages.resx的资源文件，其中包含LastNameRequured条目。  
在ASP.NET中，想要本地化资源文件，需要将当前线程的UICultrue属性设置为地域语言  

默认情况下，会在模型绑定的时候执行验证逻辑  
[HttpPost]  
public ActionResult Create(Album album){}//当操作方法带有参数的时候也会执行模型绑定  
也利用控制器的UpdateModel或TryUpdateModel显式地执行模型绑定  
模型绑定器一旦使用新值完成对模型属性的更新，就会利用当前的模型元数据获得模型的所有验证器。验证器与数据注解一同工作，会找到所有验证特性并执行验证逻辑。模型绑定器捕获所有失败的验证规则并放入模型状态中。  
模型绑定的主要副产品是模型状态(使用Controller派生类对象的ModelState属性可以访问到)。如果状态中存在错误ModelState.IsValid就返回false。  
例如，用户没有填写LastName，但LastName设置了Required验证注解，在模型绑定后，下面的表达式将返回验证结果  
ModelState.IsValid == false  
ModelState.IsValidField("LastName") == false  
ModelState["LastName"].Errors.Count > 0  
同时也可以得到失败验证的错误提示信息：  
ModelState["LastName"].Errors[0].ErrorMessage//通常很少编写代码那来查看特定的错误提示信息。HTML辅助方法  
@Html.ValidationMessageFor(m => m.LastName)  
`
[HttpPost]  
public ActionResult AddressAndPayment(Order newOrder)
{
    if(ModelState.IsValid)  
    {  
        newOrder.Username = User.Identity.Name;  
        newOrder.OrderDate = DateTime.Now;
        DB.Orders.Add(newOrder);
        DB.SaveChanges();
        return RedirectToAction("Complete",new {id=newOrder.OrderId});
    }
    return View(newOrder);
}  `  
上面这段代码立即检查ModelState.IsValid标记。模型绑定器已经构建好一个Order类对象，并用请求中的值类填充它。当模型绑定器完成更新后，就会执行有关的验证规则。也可以通过显式地调用UpdateModel或TryUpdateModel来实现，如下  
[HttpPost]  
public ActionResult AddressAndPayment(FormCollection collection)
{
    var newOrder = new Order();
    UpdateModel(newOrder);
    if(ModelState.IsValid)
    {
        newOrder.Username = User.Identity.Name;
        newOrder.OrderDate = DateTime.Now;
        DB.Orders.Add(newOrder);
        DB.SaveChanges();
        return RedirectToAction("Complete",new {id=newOrder.OrderId});
    }
    return View(newOrder);
}  
----------  
[HttpPost]  
public ActionResult AddressAndPayment(FormCollection collection)
{
    var newOrder = new Order();
    if(TryUpdateModel(newOrder))
    {
        newOrder.Username = User.Identity.Name;
        newOrder.OrderDate = DateTime.Now;
        DB.Orders.Add(newOrder);
        DB.SaveChanges();
        return RedirectToAction("Complete",new {id=newOrder.OrderId});
    }
    return View(newOrder);
}  

把验证逻辑封装在自定义数据注解中可以轻松地实现在多个模型中重用逻辑，这样需要在特性内部编写代码以应对不同类型的模型。  
另一方面，如果将验证逻辑直接放入模型对象中，就意味着验证逻辑可以很容易地实现，因为这样只需要关心一种模型对象的验证逻辑。但不利于逻辑重用  
所有的验证注解(如Required和Range)特性最终都派生自基类ValidationAttribute,它是个抽象类，在名称空间System.ComponentModel.DataAnnotations中定义。同样自定义的验证逻辑也必须派生自ValidatationAttribute的类  
using System.ComponentModel.DataAnnotation;  
namespace MvcMusicStore.Infrastructrue  
{
    public class MaxWordsAttribute : ValidationAttribute
    {
        private readonly int _maxWords;  
        //向构造函数传递一个默认的错误提示信息  
        public MaxWordsAttribute(int maxWords) : base("{0} has too many words.")
        {
            _maxWords = maxWords;
        }  
        //为了实现这个验证逻辑，至少需要重写基类中提供IsValid方法的其中一个版本  
        //第一个参数是要验证的对象，如果对象是有效的就返回一个成功的验证结果  
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if(value != null)
            {
                var valueAsString = value.ToString();
                if(valueAsString.Split(' ').Length > _maxWords)
                {
                    //FormatErrorMessage方法会使用提供的属性名来填充占位符  
                    var errorMessage = FormatErrorMessage(validationContext.DisplayName);
                    return new ValidationResult(errorMessage);
                }
            }
            return ValidationResult.Success;
        }
    }
}
应用如下  
[MaxWords(10,ErrorMessage ="There are too many words")]  
public virtual string Title { set; get; }  

IValidatableObject  
自验证(self-validating)模型是指一个知道如何验证自身的模型对象。通过实现IvalidableObject接口来对自身验证。  
public class Order : IValidatableObject
{
    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        if(LastName != null && LastName.Split(' ').Length > 10)
        {
            yield return new ValidationResult("Too many words",new []{"LastName"});
        }
    }
}
上面的代码使用了C#的yield return语法来构建枚举返回值，同时还需要显式地告知ValidationResult与其关联的字段名称  
使用自验证这种方式，当执行验证而调用的方法是Validate而不是IsValid，而且返回的类型是IEnumerable<ValidationResult>而不是单独的ValidationResult  
