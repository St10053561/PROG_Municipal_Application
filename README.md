**Municipal Services Application**
Version: 1.0
 -- Framework: .NET Framework 4.8
 -- Platform: WPF (Windows Presentation Foundation)
 -- IDE: Visual Studio 2022

**Overview**
This application is developed to streamline municipal services in South Africa. The main feature currently implemented is Report Issues, where residents can report problems related to municipal services.

**System Requirements**
To compile and run this application, you will need:

-- Operating System: Windows 10 or later
-- Development Environment: Visual Studio 2022 (Community, Professional, or Enterprise)
-- .NET Framework: Version 4.8
-- Memory: 4 GB RAM or higher
-- Disk Space: Minimum of 200 MB for the project files and dependencies

**Setup and Installation**
1. Download the Project

-- Download the .zip file of the project named 'ST10053561_Venkata Vikranth Jannatha_PROG7312_POE_PART 2' from the provided location or repository.

2. Extract the ZIP File

-- Once downloaded, extract the zip file to your desired location on your local machine.
-- Inside the extracted folder, locate the solution file: 'MunicipalService.sln'.

3. Open the Project in Visual Studio

-- Open Visual Studio 2022.
-- In Visual Studio, go to File → Open → Project/Solution....
-- Navigate to the extracted folder and select 'MunicipalService.sln' to open the project.

Compiling the Project
1. Ensure Correct Target Framework
     Ensure that the project targets .NET Framework 4.8. You can verify this by:

-- Right-clicking on the project in Solution Explorer.
-- Selecting "Properties."
-- Under the "Application" tab, check if the Target Framework is set to .NET Framework 4.8.

2. Build the Solution

-- From the top menu, go to Build → Build Solution or press Ctrl + Shift + B.
-- Visual Studio will compile the project. Ensure there are no errors in the Error List window.


** Running the Application **
1. Start the Application
Once the solution is compiled, you can run the application by:

-- Clicking on the Start button in Visual Studio or pressing F5.
-- The application will launch, and the Main Menu will appear.

2. Navigation
The application contains three main sections, but only the Report Issues feature is currently enabled.

-- Report Issues: Click this to report a problem with municipal services.
-- Local Events and Announcements: View upcoming local events and recommendations based on previous searches.
-- Service Request Status: (Disabled)


** How to Use the Application **
1. Main Menu
-- Upon startup, the user is presented with three options:
  # Report Issues (enabled)
  # Local Events and Announcements (Enabled)
  # Service Request Status (disabled)

---- Feedback Mechanism button: A confirmation dialog will ask if you want to submit another issue or return to the main menu.

--- View Report Mechanism: It allows Users to see the reports has made based on the incident.

-- Upcoming events button: it allows for user to see the upcoming events that coming up in future.

2. Report Issues Page
 - Location Input: Enter the location of the issue (uses Maps API for accurate location).
 - Category Selection: Select the type of issue (e.g., Roads, Sanitation, Utilities).
 - Description Box: Provide a detailed description of the issue.
 - Media Attachment: Attach images or documents to the report. Images are now displayed as an illustration version.
 - Delete Images: You can delete any attached image if needed.
 - Submit Button: Submits the issue report. A confirmation message will appear after a successful submission.
 - Feedback Mechanism: After submitting an issue, a confirmation dialog will prompt whether to submit another issue or return to the main menu.
 - View Reports Button: Users can view all submitted reports and track the current status of each report in this section.

3.Local Events and Announcements page
 - Search History: Tracks recent searches using a stack to recommend events based on the most recent search terms.
- Event Queue: Displays upcoming events in chronological order using a queue data structure.
- Category Filter: Filters events by unique categories stored in a hash set for fast lookups.
- Search Frequency: Uses a dictionary to track and suggest events based on frequently searched terms.
- Event Sorting: Organizes events by date using a sorted dictionary to ensure proper chronological display.
 
3. Features Implemented
   # Main Menu: Provides easy navigation between all sections.
   # Allows users to submit reports for municipal issues.
   # Uses Maps API for precise location input. 
   # Users can attach, view, and delete images or documents.
   # Feedback mechanism prompts users after submission.
   # View submitted reports and track the status.
   # Local Events and Announcements:
   # Users can search and view details for local events.
   # Recommendation system suggests related events based on search history.


