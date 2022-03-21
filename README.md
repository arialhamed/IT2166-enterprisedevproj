# enterprisedevproj

### Team 3's project by [@JakeIsMeh](https://github.com/JakeIsMeh), [@W3Sgamin](https://github.com/W3Sgamin), [@luckyappgt](https://github.com/luckyappgt) & [@arifhamed](https://github.com/arifhamed)
We are developing a website that benefits the health and well-being of the \
eldery, through events, interests, and needs that will be sponsored from \
generous donors and/or companies.

Shown below will be the changelog for our project. 

Latest build update: [Update 1.1.2](#update-120)
### Changelog

#### Alpha 1.0.0
* Repository opened by @JakeIsMeh.
* Default Razor project uploaded by @arifhamed, using Visual Studio 2019.

#### Update 1.0.16
* Multiple edits by @arifhamed, which were not documented until now.

#### Update 1.0.17
* Added changelog to [README.md](https://github.com/JakeIsMeh/enterprisedevproj/blob/master/README.md) to make things look more professional.
* Uploaded [dbo.Interests.sql](https://github.com/JakeIsMeh/enterprisedevproj/blob/master/Database/dbo.Interests.sql), for reference.
* Updated [AlertService](https://github.com/JakeIsMeh/enterprisedevproj/blob/master/Services/AlertService.cs) to support "delete".
* Added [Interest class](https://github.com/JakeIsMeh/enterprisedevproj/blob/master/Models/Interest.cs), [Interest service](https://github.com/JakeIsMeh/enterprisedevproj/blob/master/Services/InterestService.cs) and [interest creation page](https://github.com/JakeIsMeh/enterprisedevproj/blob/master/Pages/CreateInterest.cshtml), and respective changes to [Startup.cs](https://github.com/JakeIsMeh/enterprisedevproj/blob/master/Startup.cs).
* Added [ISDelete page](https://github.com/JakeIsMeh/enterprisedevproj/tree/master/Pages/IS), refering to ISMain delete buttons.
* Updated [ISMain c#](https://github.com/JakeIsMeh/enterprisedevproj/blob/master/Pages/IS/ISMain.cshtml.cs) to support delete function
* Updated [shared Layout](https://github.com/JakeIsMeh/enterprisedevproj/blob/master/Pages/Shared/_Layout.cshtml) and [site.css](https://github.com/JakeIsMeh/enterprisedevproj/blob/master/wwwroot/css/site.css) to change all of the website to use Helvetica font.
* Updated [site javascript](https://github.com/JakeIsMeh/enterprisedevproj/blob/master/wwwroot/js/site.js) for ISMain's table
* Updated ConfirmedAlert to a universal [Confirmed page](https://github.com/JakeIsMeh/enterprisedevproj/blob/master/Pages/Confirmed.cshtml) for other database appendings.
* Current issues/things to look at for @arifhamed
    * InterestService has a problem where it cannot execute function to get all interests.
    * Maybe change name of Confirmed to ConfirmedUpload

#### Update 1.0.18
* Created [Create folder](https://github.com/JakeIsMeh/enterprisedevproj/tree/master/Pages/Create) for all creation within the website
* Moved and renamed the following from Pages to Create folder:
    * CreateAlert to Alert
    * CreateInterest to Interest
* Moved Confirmed to Create folder, same named file may exist in other directories (if there is a delete directory in the future)
* Renamed and rewritten code for all items in [IS](https://github.com/JakeIsMeh/enterprisedevproj/tree/master/Pages/IS), which are the following:
    * ISMain to Main
    * ISDetail to Detail
    * ISDelete to Delete
* Added [NoIDError](https://github.com/JakeIsMeh/enterprisedevproj/blob/master/Pages/NoIDError.cshtml) for generic catching when users try to access pages without id in the OnGet parameter.
* Updated [Privacy Policy page](https://github.com/JakeIsMeh/enterprisedevproj/blob/master/Pages/Privacy.cshtml) with generic privacy policy found online. Further changes to this are expected in the future
* Current issues/things to look at for @arifhamed
    * InterestService has a problem where it cannot execute function to get all interests. (see [Update 1.0.17](#update-1017))

#### Update 1.0.19
* Updated Interests sql and added Reviews sql
    * ...and the required steps to add new table is done (mostly) (im pretty sure
* Added Errors directory to handle errors.
    * Removed NoIDError
    * Updated startup.cs so that it runs the error pages (most errors are 404 anyway so)
* Added history function to site.js
    * Not in use for now
* Current issues/things to look at:
    * See [Update 1.0.18 errors](#update-1018)
    * Decide if CRUD and confirmation pages should be seperate between IT Staff and public user, which is most of the edits in this update
    
#### Update 1.0.19b
* Found Interests db error
    * Used int type on Rating instead (sql was updated on Update 1.0.19)

#### Update 1.0.20
* Completed codes for ReviewService
* Completed 404 error page, and many other Error pages
    * 400, 401, 403, 405, 406, 408, 413, 414, 500, 502, 503, 504...
    * ...and ItemMissing that will handle what would happen if ItemExists return false in some pages
* Added Interest page
    * This page will show any given interest, and the reviews (reviews were why i created this page now)
    * So far the poster imagae is not functional
* Updated Interest sql and model
    * Added Likes
    * Added Views
        * Value will increment everytime someone views the interest page for the first time in their current session
    * Rating will show 5 stars (divided by 2 each if value not equal)
        * Will be implemented in the future
* Current issues/things to look at:
    * User db is required to ensure that Interests page will function correctly
        * including session and everything else
    * Rating display in Interests
    * @arifhamed
        * Use ideas that was recorded personally

#### Update 1.0.20b
* Updated 404 for that one open curly bracket in previous version

#### Update 1.0.20c
* Edited IS/Main, IS/Delete, Shared/_Layout & Delete/Confirmed to take screenshots for @arifhamed prototype
* For some reason, changes on local repo are recorded per session, this update was supposed to be the last one

#### Update 1.0.21
* @W3Sgamin, updated but didn't know what message to put in that git change lol, so he just put in 'a'
    * Added Events db and all necessary steps
        * Except AddTransient in Startup
    * Added Events directory
        * Added CreateEvent
        * Added ViewAll
* @arifhamed
    * Updated Events sql
        * Added CreatorId
        * Added Sponsors, which is a JSON string
    * Added EventService to transients in Startup.cs
    * Renamed the following:
        * Events/CreateEvent -> Create/Event
        * Events/ViewAll -> Read/EventAll
    * Modified Events.cs
        * to Event.cs, and every reference to the previous model
        * Renamed according to model format
    * Modified AlertService to follow a better naming format (following)
        * thealert to theAlert

#### Update 1.0.21b
* Updated EventAll temporarily for zero build errors for testing

#### Update 1.0.22
* @luckyappgt
    * Added in Needs stuff in previous unrecorded updates
* Updated Need model and sql
    * A need object would require a reference to the user needing it and also the person delivering it (*jt)
    * Also changed vegetarian to int, 0 for false, 1 for true
    * Some other stuff

#### Update 1.0.23
* @arifhamed
    * IS
        * Updated Detail to show delete option
            * Any page in IS should not be accessible to any public user except logged-in IT Staff
            * Also sharpened url handling
        * Updated Main to completely hide View Page button if Approved value is 0
        * Added InterestApproval
            * This page is just a placeholder for code (subjected to checking user privilege, just like the whole IS folder)
        * Added Approval functionality for Interests
    * Errors
        * Added InterestNotApproved
            * for if someone manually enters link to the page, but the Approved value is not 1
        * Added WrongID
            * for if someone does the above but the ID type is not right
    * Things to consider
        * Alerts should stay in the db
            * Resolved
                * What is resolved should display those that aren't
        * Any action that is done to any db that relates to any user, get the user notified (via email or seperate notification system, like Shopee)

#### Update 1.0.23b
* Updated Delete/Confirmed to redirect page to via respective role correctly
* IS
    * Updated Detail to have differing interactons
        * Alert for Interest will provide a button that will go to the actual page
    * Update Main backend nav so that it will underline which page staff is present.

#### Update 1.1.0
* @JakeIsMeh
    * EF Migration
    * Finished database creation
    * Guide to sync your local database
        * Delete any existing tables in enterprisedevproj.db
        * Enter in the following command into cmd or PowerShell in the project directory:
            dotnet ef database update
        * If dotnet ef not found
            dotnet tool install --global dotnet-ef
        * ..and try again
    * Fixed User Session.
* @arifhamed
    * Completed null views for Main
    * Added favicons (in Shared folder) (doesn't seem to work right now but)
    * Added UserService (readonly) for pages not in Identity area
    * Started work on Discover pagae 
    * IS
        * Finally dded table views for:
            * User
            * Review
            * Event
            * Need
        * Added detail views for:
            * User (different page: IS/ViewUser)
            * Review
            * Event
            * Interest
            * Need
        * Added search function for all pages
        * Updated all mentions of a user ID to also be a hyperlink to the user's IS Detail page
        
    
    * Things to take note next commit
        * Notification function
        * Work more on interests
            * Links to discover page (should resemble like a table or something)

#### Update 1.1.0b
* gitting

#### Update 1.1.0c
* Commit Update 1.1.0 didn't count these few files in so here it is.

#### Update 1.1.0d
* Dang readme didn't update.

#### Update 1.1.1
* @arifhamed
    * IS
        * Delete
            * Updated so that it (supposedly) has its email notification function working.
        * ViewUser
            * Added "warn" button (supposedly works)
    * Discover
        * Added default page from [w3schools](https://www.w3schools.com/howto/howto_css_image_grid_responsive.asp)
            * Added more stuff to the wwwroot thing.
        * Will add more functionality (and more data) to beautify it.
        * Render linkcss tag works for Discover.cshtml
    * Things to take note next commit:
        * Finish discover page 
            * Don't bother with a suggestions matrix.
            * Sort according to newest.
            * Add caption to them images.
        * Create Interest.db input (create page)
        * Add more stuff to (or rather, fix) the shared navbar.

* Ping
    * @W3Sgamin & @luckyappgt pls update your parts soon!!!!!!

#### Update 1.1.2
* Shared
    * Added jumbotron.jpg and navlogo.png but neither of them are functionally in the website yet.
    * Finally figured out how to move that search icon to the right, the left of the account things
* Index
    * moved it away from the default (for once)
        * @W3Sgamin need to look at this though
* Create
    * Added session checks for all in this directory
* IS
    * Added session checks for all, and checks for userid, for now there is only one it staff
* @arifhamed
    * Remembered that user session is fixed since update 1.1.0, so added user limiting (if that is the right phrase) for all known pages (major update)
    * Things to take note next commit:
        * Finish discover page 
            * Don't bother with a suggestions matrix.
            * Sort according to newest.
            * Add caption to them images.
        * Create Interest.db input (create page)
        * Add one more user (besides the existing staff and user) for content manager
        * bruh to oblivion with this damn project, man
        * Enable back the startupcs error pages thing

#### Update 1.1.3
@luckyappgt
* Needs
    * Shifted all Need pages into "Needs" folder
    * Added Needs in the navbar
    * Main page(Needs) only displays the needs of the current signed in user
    * Main page will show a link if user has not added any needs
    * Update form broke (need to fix this, fields not getting filled with the existing data)

#### Update 1.1.4
@luckyappgt
* Needs
    * Fixed update form getting filled with existing data
    * Added Staff pages(under Needs/Staff)
    * Updated the user's main page
    * Added pages for successful/unsuccessful update
    * Small note: BeneficiaryId and AssisantId right now is the same as the signed in user id

#### Update 1.1.5 
* @arifhamed
    * was doing smtg but forgot what it was after the previous commit
    * Something related to renaming roles and stuff
    * note for @luckyappgt
        * instead of hardcoding ids, use the email (or rather in the code, "username") instead
            * Emails that use "@resurface.org.sg" is staff, whereas anything else is are not frontend
        * ...so i changed the loginPartial as so.
    * Things to note for myself before next commit:
        * Continue to work on Discover page.
        * Continue to work on Interests page.
        * Scan through entire code.

#### Update 1.1.6
"between covid and despair, i would rather quarantine myself with hope" - naegi, probably
* @arifhamed
    * Configured Discover for hardcoded, cuz for some dang reason it was not the same as literally every other page, oh well.
    * Spiced up interest display page
        * Added new function to ReviewService for specific use (thx @luckyappgt!)
    * Things/ideas I have abandoned:
        * In review making, have the rating thing.
        * Likes in interests.
        * Poster upload and display
        * More buttons in IS/Detail

#### Update 1.1.7
Small Update
* @luckyappgt
    * Create page will now check if user has a need record in the database
        * If exists, shows the user needs exist and redirects to home page
    * Updated the update needs page to check if update is successful
* @W3Sgamin
    * Moved files to event

#### Update 1.1.8
* @arifhamed
    * Fixed interest page review making
    * Updated ReviewService so that byinterestid would return sorted by date already
    * smh now this is working again, ViewData title back to Discover page
    * Fixed redirect in Delete/Confirmed, redirects by session values
    * Updated alert model to remove minimum length on description
        * Updated IS/Main view to avoid substring error or smtg
    * Just had a great idea:
        * I could create a search page, like a totally new one. The Shopee android app does that
        * Probably not gonna finish it
            * yeah hell naw
    * Updated all services to return all items according to date by default
    * Added upcoming page for the sake of the navbar

#### Update 1.1.9
* @W3Sgamin
    * Allow users to sign up for events
    * Event Manager
        * Able to view who as signed up for the events
* @luckyappgt
    * Added validation
        * Dates
        * User cannot sign up twice on the same event lul

#### Update 1.1.10
* @arifhamed
    * Made event manager access in loginpartial more dynamic
    * Added review alert ability in interest
    * Finally made image upload for interest possible (thank god)
    * Update is/main/alerts page from recommendation
    * minor fixes here and there, i'm almost expiring, but gotta get it done all the way

#### Update 1.1.10b
* @arifhamed
    * fixed event manager access

#### Update 1.1.11
* @luckyappgt
    * Made event manager access for Event/Staff pages dynamic also
    * Delete function for events added

#### Update 1.1.11b
* forgot the README ;-;

#### Update 1.1.11c
* changed the index page a bit, to avoid comments from the supervisor

#### Update 1.2.0
* Final update
    * @arifhamed
        * Fixed ViewUser
    * @arifhamed, @JakeIsMeh, @W3Sgamin & @luckyappgt
        * Final Presentation


Until further notice, any update from here will not count towards the marks, hence Update 1.2.0 will be the final update. 
Thanks, guys!








