<%@ Page Language="C#" MasterPageFile="/MasterPage.Master" AutoEventWireup="true" CodeBehind="default.aspx.cs" Inherits="PAC.View.Page.Home" %>



<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <link href="CSS/defaultSite.css" rel="stylesheet" />
    <link href="CSS/bootstrap.min.css" rel="stylesheet" />
    <link href="CSS/bootstrap.css" rel="stylesheet" />
    <link href="CSS/MP.css" rel="stylesheet" />
    <script src="JS/jquery-3.3.1.min.js" type="text/javascript"></script>

    <style>
        @import url('https://fonts.googleapis.com/css?family=Boogaloo|Bree+Serif|Lobster|Merienda+One|PT+Sans|Righteous|Roboto+Slab|Saira|Sriracha|Yanone+Kaffeesatz');
    </style>

</asp:Content>


<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="heading absolute">
        <label class="label1" for="">Hi, We are <span style="font-family:'Merienda One', cursive; color:white">Pet Adopt</span></label>
        <br />
        <label class="para" >jk kasjkhkhkj kjashd kja kqwuie kjsdak jkhad dhk dasdaj jhda hjdjh hdjsj jd dh wuqie iu q ee q</label>
    </div>

    <div class="slider">
        <div class="slides">
            <img src="Image/MP-cat1.jpg" class="slide"/>
            <img src="Image/MP-dog1.jpg" class="slide"/>
            <img src="Image/MP-cat2.jpg" class="slide"/>
            <img src="Image/MP-dog2.jpg" class="slide"/>
            <img src="Image/MP-cat3.jpg" class="slide"/>
            <img src="Image/MP-dog3.jpg" class="slide"/>
            <img src="Image/MP-cat4.jpg" class="slide"/>
            
        </div>

    </div>

    <div class="content absolute">
        <div class="row">
            <div class="col-md-3" id="3col1">
                <asp:LinkButton ID="lkDog" runat="server" OnClick="lkDog_Click">
                    <asp:Image ID="Image1" ImageUrl="~/Image/img-dog2.jpg" CssClass="img1" runat="server"/>
                </asp:LinkButton>
                <p class="cols">This is the test img design</p>
                <span class="designer">PassionForPets, Designer</span>
            </div>

            <div class="col-md-3">
                <asp:LinkButton ID="lkCat" runat="server" OnClick="lkCat_Click">
                    <asp:Image ID="Image2" ImageUrl="~/Image/img-cat1.jpg" CssClass="img1" runat="server"/>
                </asp:LinkButton>
                <p class="cols">This is the test img design</p>
                <span class="designer">PassionForPets, Designer</span>
            </div>
            
            <div class="col-md-3" >
                <asp:LinkButton ID="lkOthers" runat="server" OnClick="lkOthers_Click">
                    <asp:Image ID="Image3" ImageUrl="~/Image/background-horse.jpg" CssClass="img1" runat="server"/>
                </asp:LinkButton>
                <p class="cols">This is the test img design</p>
                <span class="designer">PassionForPets, Designer</span>
            </div>

        </div>

    </div>
    
    <script>
        var index = 0;
        showSlides();

        function showSlides() {
            var i;
            var slides = document.getElementsByClassName("slide");
            for (i = 0; i < slides.length; i++) {
                slides[i].style.display = "none";
            }
            index++;
            if (index > slides.length) {
                index = 1;
            }
            slides[index - 1].style.display = "block";//animate({'margin-left': '-=1900'}, 1000);//style.display = "block";
            setTimeout(showSlides, 3000);

            
        }





        /** var $slides = $('.slides');
        var $slide = $('.slide');
        var index = 0;
        $(document).ready(function () {
            $slides.animate({ 'margin-left': '-=1900' }, 1300, function () {

                count++;
                if (count == $slide.length) {
                    $slides.css("margin-left", 0);
                    index = 0;
                }
            });

        },1000);
        */

        

    </script>


</asp:Content>
