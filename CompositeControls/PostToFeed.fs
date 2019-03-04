module PostToFeed

open canopy.classic
open System
open CanopyExtensions

let _postToFeedHeader = xpath "//div[contains(text(),'Post to Feed')]"
let _postTofeed = css "textarea[name='text']"
let _visibility = css "select[name='object_privacy_view']"
let _publishAt = css "input[name='date_fake']"
let _postButton = css "button[type='submit']"
let _postToFeedElement = css "#bx-form-element-text div.show-placeholder > span"

// see below clickEmojiButton
let _addEmojiButton = css ".add-emoji a"
let _joyButton = css ".emoji-items a[title=':joy:']"
let _addLinkButton = css ".add-link a"
let _addLinkTextBox = css "input[name='url']"
let _addLinkError = css "#bx-form-element-url .bx-form-warn"
let _addLinkSubmitButton = css "button[name='do_submit']"
let _addLinkCloseButton = css "button[name='do_cancel']"
let _postedMessage = css "div.bx-tl-items div.bx-tl-item:first-of-type .bx-tl-item-content p" //"#bx-timeline-main-outline-public div.bx-tl-items div.bx-tl-item:first-of-type .bx-tl-item-content p"
let _firstPostedLinkFrame = css "div.bx-tl-items div.bx-tl-item:first-of-type .bx-tl-item-content iframe"


//unfortunately it is not easy using normal selenium way to insert a text into that control.
//devs said to do this way.
let insertPostMessage message = 
    js (sprintf "$('textarea[id^=\"bx-timeline-textarea\"').froalaEditor('html.set','<p>%s</p>')" message) |> ignore

let postMessageAndVerify messageToPost = 
    insertPostMessage messageToPost
    _visibility << "Public"
    click _postButton
    _postedMessage == messageToPost

let clickEmojiButton emojiButton = 
    click _addEmojiButton
    let selector = sprintf ".emoji-items a[title='%s']" emojiButton    
    canopy.classic.waitForElement (css selector)    
    click (css selector)     

let addedLinkSection itemNumber = 
    let section = sprintf "#bx-timeline-attach-link-form_field [id^=bx-timeline-attach-link-item]:nth-child(%i)" itemNumber
    css section
