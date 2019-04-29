using EventManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventManagement.Interface
{
    public interface IContact
    {
        int AddMessage(Contact entity);
        void SaveFeedback(Feedback Feedback);
        IEnumerable<Feedback> ShowAllFeedback();
        IQueryable<ViewFeedback> ShowFeedback(string sortColumn, string sortColumnDir, string Search);
        Feedback FeedbackByID(int id);
        IQueryable<Contact> ShowMessage(string sortColumn, string sortColumnDir, string Search);
        Contact MessageByID(int id);
    }
}
