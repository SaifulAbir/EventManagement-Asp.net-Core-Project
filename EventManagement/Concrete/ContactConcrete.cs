using EventManagement.Interface;
using EventManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventManagement.Concrete
{
    public class ContactConcrete : IContact
    {
        private DatabaseContext _context;

        public ContactConcrete(DatabaseContext context)
        {
            _context = context;
        }

        public int AddMessage(Contact entity)
        {
            _context.Contact.Add(entity);
            return _context.SaveChanges();
        }
        public void SaveFeedback(Feedback Feedback)
        {
            try
            {
                _context.Feedback.Add(Feedback);
                _context.SaveChanges();
            }
            catch (Exception)
            {

                throw;
            }
        }
        public IEnumerable<Feedback> ShowAllFeedback()
        {
            return _context.Feedback.ToList();
        }

        public IQueryable<ViewFeedback> ShowFeedback(string sortColumn, string sortColumnDir, string Search)
        {
            var IQueryableVenue = (from tempfeedback in _context.Feedback
                                   select new ViewFeedback
                                   {
                                       FeedbackID = tempfeedback.FeedbackID,
                                       Createdby = tempfeedback.Createdby,
                                       Experience = tempfeedback.Experience,
                                       
                                   });


            if (!(string.IsNullOrEmpty(sortColumn) && string.IsNullOrEmpty(sortColumnDir)))
            {
                // IQueryableVenue = IQueryableVenue.OrderBy(sortColumn + " " + sortColumnDir);
            }
            if (!string.IsNullOrEmpty(Search))
            {
                IQueryableVenue = IQueryableVenue.Where(m => m.Createdby == Search);
            }

            return IQueryableVenue;
        }
        public Feedback FeedbackByID(int id)
        {
            try
            {
                Feedback feedback = _context.Feedback.Find(id);
                return feedback;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public IQueryable<Contact> ShowMessage(string sortColumn, string sortColumnDir, string Search)
        {
            var IQueryableMessage = (from tempmessage in _context.Contact
                                   select new Contact
                                   {
                                       ContactID = tempmessage.ContactID,
                                       Name = tempmessage.Name,
                                       EmailID = tempmessage.EmailID,
                                       Subject = tempmessage.Subject,
                                       Mobileno = tempmessage.Mobileno,
                                       Message = tempmessage.Message

                                   });


            if (!(string.IsNullOrEmpty(sortColumn) && string.IsNullOrEmpty(sortColumnDir)))
            {
                // IQueryableVenue = IQueryableVenue.OrderBy(sortColumn + " " + sortColumnDir);
            }
            if (!string.IsNullOrEmpty(Search))
            {
                IQueryableMessage = IQueryableMessage.Where(m => m.Name == Search);
            }

            return IQueryableMessage;
        }
        public Contact MessageByID(int id)
        {
            try
            {
                Contact contact = _context.Contact.Find(id);
                return contact;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
