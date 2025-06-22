      //          < div class= "navbar-collapse collapse d-sm-inline-flex justify-content-between" >
      //              < ul class= "navbar-nav flex-grow-1" >


      //                  < li class= "nav-item" >
      //                      < a class= "nav-link text-dark" asp - area = "" asp - page = "/Index" > Home </ a >
      //                  </ li >

      //                  @* CORRECT WAY TO DO ROLES ON PAGES*@

      //                  @*                         @if (User.IsInRole("Admin"))
      //                                          {
      //                                              <li class="nav-item">
      //                                                  <a class="nav-link text-dark" asp-area="" asp-page="/Applications/Index">New Member Application</a>
      //                                              </li>
      //                                          } *@

      //                  < li class= "nav-item" >

      //                      < a class= "nav-link text-dark" asp - area = "" asp - page = "/Applications/Create" > New Member Application</a>
						//</li>

      //                  @if (User.IsInRole("Admin") || User.IsInRole("MembershipCommittee"))
      //                  {

      //                      < li class= "nav-item" >

      //                          < a class= "nav-link text-dark" asp - area = "" asp - page = "/Applications/ApprovalIndex" > Application Approval </ a >

      //                      </ li >
      //                  }

      //                  @if(User.IsInRole("Admin") || User.IsInRole("MembershipCommittee"))
      //                  {

      //                      < li class= "nav-item" >

      //                          < a class= "nav-link text-dark" asp - area = "" asp - page = "/Applications/Index" > Index </ a >

      //                      </ li >
      //                  }


      //                  @if(User.IsInRole("Admin") || User.IsInRole("Clerk") || User.IsInRole("ProShop"))
      //                  {

      //                      < li class= "nav-item" >

      //                          < a class= "nav-link text-dark" asp - area = "" asp - page = "/TeeSheet" > TeeSheet </ a >

      //                      </ li >
      //                  }
      //                  @if(User.IsInRole("Admin") || User.IsInRole("Clerk") || User.IsInRole("Gold") || User.IsInRole("Silver") || User.IsInRole("Bronze") || User.IsInRole("ProShop"))
      //                  {

      //                      < li class= "nav-item" >

      //                              < a class= "nav-link text-dark" asp - area = "" asp - page = "/TeeTimes/Index" > Tee Time </ a >

      //                      </ li >
      //                  }


      //                  @if(User.IsInRole("Admin") || User.IsInRole("Clerk") || User.IsInRole("Gold") || User.IsInRole("ProShop"))
      //                  {

      //                      < li class= "nav-item" >

      //                          < a class= "nav-link text-dark" asp - area = "" asp - page = "/StandingTeeTimes/Index" > StandingTeeTime </ a >

      //                      </ li >
      //                  }

      //                  @if(User.IsInRole("Admin") || User.IsInRole("Clerk"))
      //                  {

      //                      < li class= "nav-item" >

      //                      < a class= "nav-link text-dark" asp - area = "" asp - page = "/Events/Index" > Events </ a >

      //                      </ li >
      //                  }


      //                  < li class= "nav-item" >
      //                      < a class= "nav-link text-dark" asp - area = "" asp - page = "/Privacy" > Privacy </ a >
      //                  </ li >

      //              </ ul >
      //              < partial name = "_LoginPartial" />
      //          </ div >
