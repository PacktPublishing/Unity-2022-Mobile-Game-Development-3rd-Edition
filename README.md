# Unity 2022 Mobile Game Development

<a href="https://www.packtpub.com/product/unity-2022-mobile-game-development-third-edition/9781804613726?utm_source=github&utm_medium=repository&utm_campaign="><img src="https://content.packt.com/B18868/cover_image_small.jpg" alt="" height="256px" align="right"></a>

This is the code repository for [Unity 2022 Mobile Game Development](https://www.packtpub.com/product/unity-2022-mobile-game-development-third-edition/9781804613726?utm_source=github&utm_medium=repository&utm_campaign=), published by Packt.

**Build and publish engaging games for Android and iOS**

## What is this book about?
Unity is a well-established player in the mobile game development sphere, and its new release, Unity 2022, is packed with new, exciting features. In Unity 2022 Mobile Game Development, the third edition in this popular series, you'll get to grips with the Unity game engine by building a mobile game and publishing it on the most popular mobile app stores as well as exploring the all-new features.

This book covers the following exciting features:
* Design responsive UIs for your mobile games
* Detect collisions, receive user input, and create player movements
* Create engaging gameplay elements using mobile device input
* Add custom icons and presentation options
* Keep players engaged by using Unity's mobile notification package
* Integrate social media into your projects
* Incorporate augmented reality features in your game for real-world appeal
* Build exciting games with post-processing and particle effects

If you feel this book is for you, get your [copy](https://www.amazon.com/dp/180461372X) today!

<a href="https://www.packtpub.com/?utm_source=github&utm_medium=banner&utm_campaign=GitHubBanner"><img src="https://raw.githubusercontent.com/PacktPublishing/GitHub/master/GitHub.png" 
alt="https://www.packtpub.com/" border="5" /></a>

## Instructions and Navigations
All of the code is organized into folders. For example, Chapter02.

The code will look like the following:
```
public void ShowNotification(string title, string body,
                                DateTime deliveryTime)
{
    IGameNotification notification =
    notificationsManager.CreateNotification();
    
    if (notification != null)
    {
        notification.Title = title;
        notification.Body = body;
        notification.DeliveryTime = deliveryTime;
        notification.SmallIcon = "icon_0";
        notification.LargeIcon = "icon_1";
        
        notificationsManager.ScheduleNotification(notification);
    }
}
```

**Following is what you need for this book:**
If you are a game developer or mobile developer looking to learn Unity and employ it to build mobile games for iOS and Android, then this Unity book is for you. Prior knowledge of C# and Unity will be beneficial but isn't mandatory.

With the following software and hardware list you can run all code files present in the book (Chapter 1-15).
### Software and Hardware List
| Chapter | Software required | OS required |
| -------- | ------------------------------------ | ----------------------------------- |
| 1-15 | Unity 2022.1.0b16 | Windows, Mac OS X, and Linux |
| 1-15 | Unity Hub 3.3.1 | Windows, Mac OS X, and Linux |

We also provide a PDF file that has color images of the screenshots/diagrams used in this book. [Click here to download it](https://packt.link/6M4wR).

### Related products
* Learning C# by Developing Games with Unity - Seventh Edition [[Packt]](https://www.packtpub.com/product/learning-c-by-developing-games-with-unity-seventh-edition/9781837636877?utm_source=github&utm_medium=repository&utm_campaign=9781837636877) [[Amazon]](https://www.amazon.com/dp/1837636877)

* Hands-On Unity 2022 Game Development - Third Edition [[Packt]](https://www.packtpub.com/product/hands-on-unity-2022-game-development-third-edition/9781803236919?utm_source=github&utm_medium=repository&utm_campaign=9781803236919) [[Amazon]](https://www.amazon.com/dp/1803236914)

## Get to Know the Author
**John P. Doran** is a passionate and seasoned technical game designer, software engineer, and author who is based in Songdo, South Korea. His passion for game development began at an early age. He later graduated from DigiPen Institute of Technology with a bachelor of science in game design and a master of science in computer science from Bradley University.
For over a decade, John has gained extensive hands-on expertise in game development, working in various roles ranging from game designer to lead user interface (UI) programmer, working in teams consisting of just himself to over 70 people in student, mod, and professional game projects, including working at LucasArts on Star Wars: 1313. Additionally, John has worked in game development education, teaching in Singapore, South Korea, and the US. To date, he has authored 17 books pertaining to game development and is a 2023 Unity Education Ambassador.
John is currently an instructor at George Mason University Korea. Prior to his present ventures, he was an award-winning videographer.

## Other books by the author
* [Unity 2017 Mobile Game Development](https://www.packtpub.com/product/unity-2017-mobile-game-development/9781787288713?utm_source=github&utm_medium=repository&utm_campaign=9781787288713)

* [Unity 2020 Mobile Game Development - Second Edition](https://www.packtpub.com/product/unity-2020-mobile-game-development-second-edition/9781838987336?utm_source=github&utm_medium=repository&utm_campaign=9781838987336)

* [Unreal Engine Game Development Cookbook](https://www.packtpub.com/product/unreal-engine-game-development-cookbook/9781784398163?utm_source=github&utm_medium=repository&utm_campaign=9781784398163)

* [Unity 2021 Shaders and Effects Cookbook - Fourth Edition](https://www.packtpub.com/product/unity-2021-shaders-and-effects-cookbook-fourth-edition/9781839218620?utm_source=github&utm_medium=repository&utm_campaign=9781839218620)

* [Unity 2018 Shaders and Effects Cookbook - Third Edition](https://www.packtpub.com/product/unity-2018-shaders-and-effects-cookbook-third-edition/9781788396233?utm_source=github&utm_medium=repository&utm_campaign=9781788396233)

* [Unreal Engine 4.x Scripting with C++ Cookbook](https://www.packtpub.com/product/unreal-engine-4x-scripting-with-c-cookbook-second-edition/9781789809503)

* [Building an FPS Game with Unity](https://www.packtpub.com/product/building-an-fps-game-with-unity/9781782174806)
