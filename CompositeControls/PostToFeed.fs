module PostToFeed

open canopy.classic


let _postTofeed = css "textarea[name='text']"
let _visibility = css "select[name='object_privacy_view']"
let _publishAt = css "input[name='date_fake']"
let _postButton = css "button[type='submit']"
let _postToFeedElement = "#bx-form-element-text div.show-placeholder > span"
let _postedMessage = css "#bx-timeline-main-outline-public div.bx-tl-items div.bx-tl-item:first-of-type .bx-tl-item-content p"
let _hintButton = "footer ul li a.shepherd-button-primary"


//unfortunately it is not easy using normal selenium way to insert a text into that control.
//devs said to do this way.
let insertPostMessage message = 
    js (sprintf "$('textarea[id^=\"bx-timeline-textarea\"').froalaEditor('html.set','<p>%s</p>')" message) |> ignore
