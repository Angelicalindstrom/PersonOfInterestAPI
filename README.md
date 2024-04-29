PersonOfInterestAPI - Labb3


'PERSONS'

GET/persons - get all Persons
POST/persons - add new person


'INTERESTS'

GET/interests - Get all Interests
POST/interests - add new Interest


'PERSONINTERESTS'
FkPersonsId & FkInterestsId


GET/personinterests - get all PersonInterests
POST/personinterests - add new PersonInterest 
( you can also POST a new person and a new Interest at the same time, while adding new PersonInterest)
{
  "personInterestId": 0,
  "fkPersonId": 0,
  "person": {
    "personId": 0,
    "personName": "Jennie",
    "personEmail": "jennie@ gmail .com",
    "personPhoneNumber": "0745855669"
  },
  "fkInterestId": 0,
  "interest": {
    "interestId": 0,
    "interestTitle": "Cows",
    "interestDescription": "muuuuuuuu"
  }
}

GET/personinterests/{FkPersonId} - get all PersonInterests connected to specifik FkPersonId


'LINKS'
FkPersonInterests

GET/links - get all Links
POST/links - add new links, connect to person and interest using FkPersonInterest
(You can also add a new Person, new Interest and new PersonInterest at the same time, while adding new Link)
{
  "linkId": 0,
  "linkUrl": "https://www.  marvel . com/",
  "fkPersonInterestId": 0,
  "personInterest": {
    "personInterestId": 0,
    "fkPersonId": 0,
    "person": {
      "personId": 0,
      "personName": "Matteus",
      "personEmail": "matteus @gmail . com",
      "personPhoneNumber": "0745555555"
    },
    "fkInterestId": 0,
    "interest": {
      "interestId": 0,
      "interestTitle": "Marvel",
      "interestDescription": "a whole new universe"
    }
  }
}


GET/links/{FkPersonId} - get all links connected to specifik FkPersonId
