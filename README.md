# AI
While focusing on this issue, we actually thought that environmental pollution could be prevented at least a little. Today, we see that with the increase in consumption, garbage and pollution also increase. For this reason, we decided to create a system that detects garbage and its nature
prepared a basic user interface to create a user-friendly experience. The user can upload the item they want to query to the system and receive visual feedback for better understanding and information.
When the garbage to be classified is loaded into the interface, the image uploaded by the user is displayed on the page. Then, when the classify button is pressed, the image is sent to our python code to be classified. Then, the classification result is shown to the user both with its accuracy value and which class it belongs to.

As a result, we have a program trained with our dataset in CNN architecture and an interface through which we can transfer it to the user. In this way, we can easily view both accuracy rates and classification results.

Unlike others who use this architecture to classify, we expanded both our data set and our test data set so that we can get higher accuracy rates. Additionally, to speed up classification, we embedded it in a program that we can train once and use multiple times, rather than running the program again.

