# Municipal Services Application

**Version**: 1.0  
**Framework**: .NET Framework 4.8  
**Platform**: WPF (Windows Presentation Foundation)  
**IDE**: Visual Studio 2022  

The Municipal Services Application is designed to streamline municipal services in South Africa, providing residents with tools to report issues, view local events, and track service request statuses efficiently.


---

## 📋 System Requirements

- **Operating System**: Windows 10 or later
- **Development Environment**: Visual Studio 2022 (Community, Professional, or Enterprise)
- **.NET Framework**: Version 4.8
- **Memory**: 4 GB RAM or higher
- **Disk Space**: Minimum 200 MB for project files and dependencies

---

## 🚀 Setup and Installation Guide

Follow these steps to set up and run the **Municipal Services Application**:

### 1. **Clone the GitHub Repository**  

1. Open your browser and visit the repository:  
   [Municipal Services Application GitHub Repository](https://github.com/St10053561/PROG_Municipal_Application)  
   
2. Click the **Code** button (green dropdown).  
3. Select **Download ZIP** to save the project files locally.  
4. Extract the downloaded file (**`PROG_Municipal_Application-main.zip`**) to a preferred location.  
5. Locate the solution file: **`MunicipalService.sln`**.

### 2. **Open the Project in Visual Studio**  

1. Launch **Visual Studio 2022**.  
2. Navigate to **File** → **Open** → **Project/Solution**.  
3. Browse to the folder where you extracted the project and select **`MunicipalService.sln`**.  

### 3. **Manual Download (Alternative Method)**  

1. Download the file named:  
   **`ST10053561_Venkata Vikranth Jannatha_PROG7312_POE_PART 3.zip`** from the provided location.  
   
2. Extract the ZIP file to your local system.  
3. Locate the solution file: **`MunicipalService.sln`**.  
4. Open it using **Visual Studio 2022** by following the steps in Section 2.  


## 🔧 Compiling the Project

1. **Ensure Target Framework**
   - Verify the project targets **.NET Framework 4.8**:
     - Right-click the project in Solution Explorer.
     - Select **Properties** → **Application** tab → Check the **Target Framework**.

2. **Build the Solution**
   - From the top menu, choose **Build** → **Build Solution** or press `Ctrl + Shift + B`.
   - Ensure no errors appear in the **Error List** window.

---

## 💻 Running the Application

1. **Start the Application**
   - Click the **Start** button in Visual Studio or press `F5`.
   - The application will launch, displaying the **Main Menu**.

2. **Navigation**
   - Main Menu sections:
     - **Report Issues** (enabled): Report issues with municipal services.
     - **Local Events and Announcements** (enabled): View and search for local events.
     - **Service Request Status** (enabled): View your Status of A Report and Report Information

## 🛠️ How to Use the Application

### **Main Menu**
   - **Report Issues**: Enables users to report municipal issues.
   - **Local Events and Announcements**: Displays upcoming events and recommendations based on user search history.
   - **Service Request Status**: It displays the information of a issue report and the status of a report with functionalities with status tracking.
   - **Feedback Mechanism**: Prompts if the user wants to submit another issue or return to the Main Menu.
     
   ### **Report Issues Page**
   - **Location Input**: Enter the issue location using Maps API for accuracy.
   - **Category Selection**: Select the issue type (e.g., Roads, Sanitation, Utilities).
   - **Description Box**: Provide a detailed description of the issue.
   - **Media Attachment**: Attach images/documents to the report. Images are displayed as illustrations.
   - **Prority Checkbox**: It allows to set a prority checked to a report or not.
   - **Delete Images**: Allows deletion of any attached image.
   - **Submit Button**: Submits the issue report and displays a confirmation message.
   - **View Reports**: View all submitted reports with tracking information.

### **Local Events and Announcements Page**
   - **Search History**: Uses a stack to recommend events based on recent searches.
   - **Event Queue**: Shows events chronologically using a queue.
   - **Category Filter**: Filters events by category using a hash set for fast lookups.
   - **Search Frequency**: Recommends events based on search frequency using a dictionary.
   - **Event Sorting**: Displays events in date order with a sorted dictionary.

### **Status Update Status Report**
   - **Initialization and Report Loading**: Loads reports from a temporary file and initializes data structures.
   - **Data Structures**: Utilizes a Binary Search Tree for efficient report searching and a Min-Heap for priority management.
   - **User Interaction**: Allows users to select reports and view detailed information, including attachments.
   - **File Handling**: Displays file icons based on the type of attachments, differentiating images from other files.
   - **Search Functionality**: Enables searching for reports by number with validation and feedback for non-existent reports.
   - **Filtering Reports**: Provides options to filter by high priority and reset to show all reports.
   - **UI Management**: Manages visibility and state of UI elements based on user interactions.
   - **Event Handling**: Responds to user actions through event handlers for buttons and list selections.

---
## 📊 Implementation Data Structure Report on Service Status

1. **ObservableCollection<IssueReport>**
 - Role: This Collection is designed for the purpose of saving users' submitted reports. It enables expansion and contraction of its size. And it is effiency whenever there is an item added or removed. 
 - Contribution to Efficiency: The ObservableCollection allows the ListBox in WPF to bind data, such that when there are changes in the collection, it reflects the changes in the user interface. This is important for the functionality called "Service Request Status" since the user must be able to view the reports they have submitted immediately after any change is made on them. It also shows the not only the list of reports but also the prority or non-prority reports to differentate on report prorities. It also makes easier to show the information for report detailing.

2. **BinarySearchTree (BST)**
 - Role: The Binary Search Tree is intended to organize issue reports in a coherent manner, which supports efficient searching based on report numbers.
 - Contribution to Efficiency: The BST allows for search operations to be implemented with O(log n) time complexity. This is especially advantageous in cases when a user requests a report by its number since details of the report can be accessed fast without going through all the reports in the list. this makes faster search on that tons of report also it shows the pop up information on that report number that user searched.

3. **MinHeap**
 - Role: The application uses a MinHeap data organization structure to keep issue reports in a hierarchy from the one with the least level of severity to the highest. The structure allows for quick retrieval of the most severe (high priority) reports so that urgent issues can be addressed without delays.
 - Contribution to Efficiency: The MinHeap guarantees that any new report can be inserted in O(log n) time, which implies that the addition of a new report becomes efficient, even when the number of reports increases. Furthermore, the minimum element (which is usually the highest priority report) can also be accessed in O(1) time indicating that it can be accessed at once. This kind of efficiency is very important in regard to the “Service Request Status” option because it allows the software to quickly load the most relevant reports for the user and help them in responding to burning issues more readily, especially those pertaining to the city council.

4. **Graph**
 - Role: The primary purpose of the ReportGraph is to assist in understanding the various relationships existing between a set of issue reports. Picture it like a network where one issue report can be based on another. This way, when a new issue is reported, the application checks for interrelations between the new issue and the existing ones and helps organize workloads easily.
 - Contribution to Efficiency: Having an idea of these relationships allows the application to assess how many days it would take to resolve every report which would also depend on its significance. For instance, if report A is critical, and report B is critical for C which is dependent on report A, then the critical report A can be worked on first. This is important so that all the critical issues are raised and dealt with in a timely manner enhancing the efficiency of the entire system.

---
## 📝 Features Implemented

- **Main Menu**: Easy navigation between sections.
- **Report Issues**:
  - Submit and track municipal issues.
  - Location input with Maps API integration.
  - Media attachment and image management.
  - Feedback prompts and report tracking.
- **Local Events and Announcements**:
  - Event search, view, and recommendation features.
  - Sorted, categorized, and frequently searched events
- **Service Status Report**:
   - Report Search , High Prority Report Search
   - Detailed information on each Report
   - Checking the Status of a Report

