# Municipal Services Application

**Version**: 1.0  
**Framework**: .NET Framework 4.8  
**Platform**: WPF (Windows Presentation Foundation)  
**IDE**: Visual Studio 2022  

The Municipal Services Application helps streamline municipal services in South Africa by allowing residents to report issues, view local events, and track municipal service statuses.

---

## 📋 System Requirements

- **Operating System**: Windows 10 or later
- **Development Environment**: Visual Studio 2022 (Community, Professional, or Enterprise)
- **.NET Framework**: Version 4.8
- **Memory**: 4 GB RAM or higher
- **Disk Space**: Minimum 200 MB for project files and dependencies

---

## 🚀 Setup and Installation

### 1. **Download the Project**
   - Download the `.zip` file named **`ST10053561_Venkata Vikranth Jannatha_PROG7312_POE_PART 2`** from the specified repository or location.

### 2. **Extract the ZIP File**
   - Extract the downloaded ZIP to a local folder.
   - Locate the solution file: **`MunicipalService.sln`**.

### 3. **Open in Visual Studio**
   - Launch Visual Studio 2022.
   - Go to **File** → **Open** → **Project/Solution**.
   - Navigate to the extracted folder and select **`MunicipalService.sln`**.

---

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
     - **Service Request Status** (disabled).

---

## 🛠️ How to Use the Application

### **Main Menu**
   - **Report Issues**: Enables users to report municipal issues.
   - **Local Events and Announcements**: Displays upcoming events and recommendations based on user search history.
   - **Service Request Status**: Disabled (feature in progress).

   - **Feedback Mechanism**: Prompts if the user wants to submit another issue or return to the Main Menu.
   - **View Reports**: Displays submitted reports with incident details and status tracking.
   - **Upcoming Events**: Shows upcoming local events in chronological order.

### **Report Issues Page**
   - **Location Input**: Enter the issue location using Maps API for accuracy.
   - **Category Selection**: Select the issue type (e.g., Roads, Sanitation, Utilities).
   - **Description Box**: Provide a detailed description of the issue.
   - **Media Attachment**: Attach images/documents to the report. Images are displayed as illustrations.
   - **Delete Images**: Allows deletion of any attached image.
   - **Submit Button**: Submits the issue report and displays a confirmation message.
   - **View Reports**: View all submitted reports with tracking information.

### **Local Events and Announcements Page**
   - **Search History**: Uses a stack to recommend events based on recent searches.
   - **Event Queue**: Shows events chronologically using a queue.
   - **Category Filter**: Filters events by category using a hash set for fast lookups.
   - **Search Frequency**: Recommends events based on search frequency using a dictionary.
   - **Event Sorting**: Displays events in date order with a sorted dictionary.

---

## 📝 Features Implemented

- **Main Menu**: Easy navigation between sections.
- **Report Issues**: Submit and track municipal issues.
  - Location input with Maps API integration.
  - Media attachment and image management.
  - Feedback prompts and report tracking.
- **Local Events and Announcements**:
  - Event search, view, and recommendation features.
  - Sorted, categorized, and frequently searched events.
