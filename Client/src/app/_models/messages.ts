export interface messages {
  id: number
  senderId: number
  senderUserName: string
  senderPhotoUrl: string
  recipientPhotoUrl: any
  recipientId: number
  recipientUserName: string
  content: string
  dateReadd?: Date
  messageSent: Date
}
